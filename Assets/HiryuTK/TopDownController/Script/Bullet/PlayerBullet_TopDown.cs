﻿using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// Class for controlling the player's bullet
    /// </summary>
    public class PlayerBullet_TopDown : PoolObject
    {
        //References
        private Settings_TopDownController settings;
        private Rigidbody2D rb;

        #region Object pool
        public override void InitialSpawn(Pool pool)
        {
            //Set the object's pool reference, then reference classes and components
            base.InitialSpawn(pool);
            settings = Settings_TopDownController.Instance;
            rb = GetComponent<Rigidbody2D>();
        }

        public override void Activation(Vector2 p, Quaternion r)
        {
            //When this object is spawned, give it the proper velocity towards its up direction
            base.Activation(p, r);
            rb.velocity = transform.up * settings.BasicBullet_speed;
        }
        #endregion

        private void FixedUpdate()
        {
            //Despawn this object when it goes outside the screen
            if (settings.IsOutOfBounds(transform.position))
            {
                Despawn();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Make this able to damage enemies and asteroids upon collision
            if (Settings_TopDownController.Instance.IsTargetOnEnemyLayer(collision.gameObject) ||
                Settings_TopDownController.Instance.IsTargetOnGroundLayer(collision.gameObject))
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(1);
                Despawn();
            }
        }
    }
}