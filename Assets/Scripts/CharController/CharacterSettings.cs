using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-9000)] 
public class CharacterSettings : MonoBehaviour
{
    public static CharacterSettings instance { get; private set; }

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    public LayerMask GroundLayer => groundLayer;
    public LayerMask EnemyLayer => enemyLayer;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 1f; //50f
    [SerializeField] private float steerSpeed = 1f; //50f
    public float MoveSpeed => moveSpeed;
    public float SteerSpeed => steerSpeed;


    [Header("Hurt State")]
    [Range(10f, 50f)] [SerializeField] private float hurtSlideSpeed = 20f; //50f

    [SerializeField] private Vector3 hurtDirection = new Vector3(0f, 25f, 20f);
    [SerializeField] private float hurtDuration = 0.5f;
    public float HurtSlideSpeed => hurtSlideSpeed;
    public Vector2 HurtDirection => hurtDirection;
    public float HurtDuration => hurtDuration;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
    }
}