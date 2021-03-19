using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-90000000)]
    public class Settings_TopDownController : MonoBehaviour
    {
        public static Settings_TopDownController Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int playerHealth = 3;
        public LayerMask PlayerMaxHealth => playerHealth;

        [Header("Abilities")]
        [SerializeField] private float basicBullet_speed = 20f;
        [SerializeField] private float cd_BasicAttack = 1f;
        [SerializeField] private float cd_Mining = 0.1f;
        [SerializeField] private float   miningPower = 5f;
        [SerializeField] private float miningDistance = 50f;
        public float BasicBullet_speed => basicBullet_speed;
        public float CD_BasicAttack => cd_BasicAttack;
        public float CD_Mining => cd_Mining;
        public float MiningPower => miningPower;
        public float MiningDistance => miningDistance;

        [Header("Layers")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;

        [Header("Player Movement")]
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float moveAcceleration = 1f;
        [SerializeField] private float rotationSpeed = 20f; //50f
        [SerializeField] private float rotationAccleration= 1f; //50f
        public float MoveSpeed => moveSpeed;
        public float MoveAcceleration => moveAcceleration;
        public float RotationSpeed => rotationSpeed;
        public float RotationAccleration => rotationAccleration;
        
        [Header("Enemy Movement")]
        [SerializeField] private float enemyMove = 2f;
        [SerializeField] private float enemyRotation = 100f;
        public float EnemyMove => enemyMove;
        public float EnemyRotation => enemyRotation;

        [Header("Asteroid Movement")]
        [SerializeField] private float asteroidMove =  15f;
        [SerializeField] private float asteroidRotation = 10f;
        public float AsteroidMove => asteroidMove;
        public float AsteroidRotation => asteroidRotation;

        public float ScreenBound_Top { get; private set; }
        public float ScreenBound_Bot { get; private set; }
        public float ScreenBound_Left { get; private set; }
        public float ScreenBound_Right { get; private set; }
        public float innerSideline_Top { get; private set; }
        public float innerSideline_Bot { get; private set; }
        public float innerSideline_Left { get; private set; }
        public float innerSideline_Right { get; private set; }
        public float outerSideline_Top { get; private set; }
        public float outerSideline_Bot { get; private set; }
        public float outerSideline_Left { get; private set; }
        public float outerSideline_Right { get; private set; }


        //Cache for spawn point calculation
        private float[] xSubPoints;
        private float[] ySubPoints;

        private void Awake()
        {
            Instance = this;

            Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f));
            Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            ScreenBound_Top = upperRight.y;
            ScreenBound_Bot = lowerLeft.y;
            ScreenBound_Left = lowerLeft.x;
            ScreenBound_Right = upperRight.x;

            float offset = .4f;
            innerSideline_Top       = ScreenBound_Top   + offset;
            innerSideline_Bot       = ScreenBound_Bot   - offset;
            innerSideline_Left      = ScreenBound_Left  - offset;
            innerSideline_Right     = ScreenBound_Right + offset;

            float offsetSmall = 1f;
            outerSideline_Top       = innerSideline_Top     + offsetSmall;
            outerSideline_Bot       = innerSideline_Bot     - offsetSmall;
            outerSideline_Left      = innerSideline_Left    - offsetSmall;
            outerSideline_Right     = innerSideline_Right   + offsetSmall;

            Debug.DrawLine(new Vector2(outerSideline_Left, outerSideline_Top), new Vector2(outerSideline_Right, outerSideline_Top), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Left, outerSideline_Top), new Vector2(outerSideline_Left, outerSideline_Bot), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Right, outerSideline_Bot), new Vector2(outerSideline_Left, outerSideline_Bot), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Right, outerSideline_Bot), new Vector2(outerSideline_Right, outerSideline_Top), Color.red, 10f);

            Debug.DrawLine(new Vector2(innerSideline_Left, innerSideline_Top), new Vector2(innerSideline_Right, innerSideline_Top), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Left, innerSideline_Top), new Vector2(innerSideline_Left, innerSideline_Bot), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Right, innerSideline_Bot), new Vector2(innerSideline_Left, innerSideline_Bot), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Right, innerSideline_Bot), new Vector2(innerSideline_Right, innerSideline_Top), Color.white, 10f);

            //Initialize the subdivision points in the middle of the zone.
            int xDivisions = 4;
            float xdivisionDist = (ScreenBound_Right - ScreenBound_Left) / xDivisions;
            xSubPoints = new float[xDivisions - 1];
            for (int i = 0; i < xDivisions - 1; i++)
            {
                xSubPoints[i] = ScreenBound_Left + xdivisionDist * (1 + i);
            }

            int yDivisions = 4;
            float ydivisionDist = (ScreenBound_Top - ScreenBound_Bot) / yDivisions;
            ySubPoints = new float[yDivisions - 1];
            for (int i = 0; i < yDivisions - 1; i++)
            {
                ySubPoints[i] = ScreenBound_Bot + ydivisionDist * (1 + i);
            }

            //Debug 
            for (int x = 0; x < xSubPoints.Length; x++)
            {
                for (int z = 0; z < ySubPoints.Length; z++)
                {
                    Vector2 p = new Vector2(xSubPoints[x], ySubPoints[z]);
                    Debug.Log(p);
                    Debug.DrawLine(p, p + Vector2.right, Color.magenta, 10f);
                }
            }
        }

        public bool IsTargetOnPlayerLayer(GameObject go) => PlayerLayer == (PlayerLayer | 1 << go.layer);
        public bool IsTargetOnEnemyLayer(GameObject go) => EnemyLayer == (EnemyLayer | 1 << go.layer);
        public bool IsTargetOnGroundLayer(GameObject go) => GroundLayer == (GroundLayer | 1 << go.layer);

        public Vector2 RandomSpawnPoint ()
        {
            if (RandomBool)
            {
                //Spawn top and bottom
                float x = Random.Range(ScreenBound_Left, ScreenBound_Right);
                float y = RandomBool ? innerSideline_Bot : innerSideline_Top;
                return new Vector2(x, y);
            }
            else
            {
                //Spawn left and right edge
                float y = Random.Range(ScreenBound_Top, ScreenBound_Bot);
                float x = RandomBool ? innerSideline_Left : innerSideline_Right;
                return new Vector2(x, y);
            }
        }

        public Quaternion RandomSpawnRotation (Vector2 spawnPoint)
        {
            Vector2 aim = new Vector2(
                xSubPoints[Random.Range(0, xSubPoints.Length)], 
                ySubPoints[Random.Range(0, ySubPoints.Length)]);
            return Quaternion.LookRotation(Vector3.forward, aim - spawnPoint);
        }

        public bool TryGetScreenWarpPosition (Vector2 currentPos, out Vector2 warpPos)
        {
            warpPos = currentPos;
            //Warp Left >>> Right
            if (currentPos.x < innerSideline_Left)
            {
                warpPos.x = innerSideline_Right - .01F;
                return true;
            }
            //Warp Right >>> Left
            if (currentPos.x > innerSideline_Right)
            {
                warpPos.x = innerSideline_Left + .01F;
                return true;
            }

            //Warp Bot >>> Top
            if (currentPos.y < innerSideline_Bot)
            {
                warpPos.y = innerSideline_Top - .01F;
                return true;
            }

            //Warp Top >>> Bot
            if (currentPos.y > innerSideline_Top)
            {
                warpPos.y = innerSideline_Bot + .01F;
                return true;
            }
            return false;
        }

        public bool IsOutOfBounds(Vector2 position)
        {
            if (position.x < outerSideline_Left || position.x > outerSideline_Right ||
                position.y < outerSideline_Bot  || position.y > outerSideline_Top)
            {
                return true;
            }
            return false;
        }

        bool RandomBool => Random.Range(0, 2) == 0;
    }
}