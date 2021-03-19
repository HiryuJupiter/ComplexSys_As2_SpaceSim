using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-100)]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerTopDown3DController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform shootPoint;
        [SerializeField] private LineRenderer lineRenderer;

        //Class and components
        private Settings_TopDownController settings;
        private UIManager_TopDown uiM;

        private Rigidbody2D rb;

        //Status 
        private int Money;
        public Vector2 velocity;
        public float rotAmount;

        public static PlayerTopDown3DController Instance { get; private set; }
        public Transform ShootPoint => shootPoint;
        public LineRenderer LineRenderer => lineRenderer;
        #endregion

        #region MonoBehiavor
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            settings = Settings_TopDownController.Instance;
            uiM = UIManager_TopDown.Instance;

            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ScreenWarp();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateRotation();
            ExecuteRigidbodyVelocity();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Settings_TopDownController.Instance.IsTargetOnEnemyLayer(collision.gameObject) || 
                Settings_TopDownController.Instance.IsTargetOnGroundLayer(collision.gameObject))
            {
                Debug.Log("player hits damaging object");
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(1);
                DamagePlayer(collision.gameObject.transform.position, 999);
            }
        }
        #endregion


        #region Private
        public void AddMoney(int newAmount)
        {
            Money = Money + newAmount;
            uiM.SetMoney(Money);
        }

        public void DamagePlayer(Vector2 enemyPos, int damage)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ExecuteRigidbodyVelocity()
        {
            rb.velocity = transform.TransformDirection(velocity);
        }

        private void UpdateMovement()
        {
            float drive = Mathf.Clamp(GameInput_TopDownController.MoveY, 0f, 1f);

            velocity.y = Mathf.Lerp(velocity.y, drive * settings.MoveSpeed,
                Time.deltaTime * settings.MoveAcceleration);
        }

        private void UpdateRotation()
        {
            rotAmount = Mathf.Lerp(rotAmount, GameInput_TopDownController.MoveX, settings.RotationAccleration * Time.deltaTime);
            rb.rotation = rotAmount * settings.RotationSpeed * Time.deltaTime;
        }

        private void ScreenWarp ()
        {
            if (settings.TryGetScreenWarpPosition(transform.position, out Vector2 warpPosition))
            {
                transform.position = warpPosition;
            }
        }
        #endregion
    }
}