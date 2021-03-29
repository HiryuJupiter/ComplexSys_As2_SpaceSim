using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// Class for handling player shooting
    /// </summary>
    public class PlayerShootingModule : MonoBehaviour
    {
        //Fields
        private ObjectPoolManager_TopDown poolM;
        private PlayerTopDown3DController player;
        private Settings_TopDownController settings;
        private float shootCooldownTimer;
        private bool isSetup;

        /// <summary>
        /// Setting up the method
        /// </summary>
        /// <param name="player"> pass in reference to the player</param>
        public void Setup(PlayerTopDown3DController player)
        {
            //Reference and initialization
            this.player = player;
            poolM = ObjectPoolManager_TopDown.Instance;
            settings = Settings_TopDownController.Instance;
            isSetup = true;
        }

        /// <summary>
        /// Ticks the update for this class
        /// </summary>
        public void TickUpdate()
        {
            //Guard statement
            if (!isSetup)
                return;

            //Shoot when these keys are down
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
            {
                if (ShootingCooldownReady)
                    Shoot();
            }

            //Tick timer 
            if (!ShootingCooldownReady)
            {
                shootCooldownTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Shoot at the mouse pointing location
        /// </summary>
        private void Shoot()
        {
            Quaternion r = Quaternion.LookRotation(Vector3.forward,
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.ShootPoint.position);

            poolM.SpawnPlayerBullet(player.ShootPoint.position, r);
            ResetTimer();

        }

        private bool ShootingCooldownReady => shootCooldownTimer <= 0f;

        private void ResetTimer() => shootCooldownTimer = settings.CD_Mining;
    }
}
