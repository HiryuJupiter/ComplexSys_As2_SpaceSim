using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// Object pool for prefabs used in this scene
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class ObjectPoolManager_TopDown : MonoBehaviour
    {
        //Fields
        public static ObjectPoolManager_TopDown Instance;

        [Header("Pf")]
        [SerializeField] private PoolObject pf_playerBullet;
        [SerializeField] private PoolObject pf_enemyShip;
        [SerializeField] private PoolObject pf_Asteroid;
        public Pool playerBullet { get; private set; }
        public Pool enemyShip { get; private set; }
        public Pool asteroids { get; private set; }


        void Awake()
        {
            //Lazy singleton
            Instance = this;

            //Initialize pool for each prefab
            playerBullet = new Pool(pf_playerBullet, transform);
            enemyShip = new Pool(pf_enemyShip, transform);
            asteroids = new Pool(pf_Asteroid, transform);
        }

        //Public 
        public PoolObject SpawnPlayerBullet(Vector3 p, Quaternion r) => playerBullet.Spawn(p, r);
        public PoolObject SpawnEnemyShip(Vector3 p, Quaternion r) => enemyShip.Spawn(p, r);
        public PoolObject SpawnAsteroid(Vector3 p, Quaternion r) => asteroids.Spawn(p, r);
    }
}