using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectileBehavior : MonoBehaviour
    {
        private int damage;
        private float projectileSpeed;
        private float liveTime = 0;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rb.AddForce(transform.up * projectileSpeed);
            liveTime += Time.deltaTime;
            if (liveTime > 5)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                collider.gameObject.GetComponent<Enemy.EnemyTakeDamage>().TakeDamage(damage);
            }
        }

        public void SetDamage(int newDamage)
        {
            damage = newDamage;
        }

        public void SetProjectileSpeed(float newProjectileSpeed)
        {
            projectileSpeed = newProjectileSpeed;
        }
    }

}
