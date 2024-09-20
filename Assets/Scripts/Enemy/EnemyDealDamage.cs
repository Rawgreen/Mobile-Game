using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyDealDamage : MonoBehaviour
    {
        private int damage;
        private EnemyAttributes enemyAttributes;

        public void Awake()
        {
            enemyAttributes = GetComponent<EnemyAttributes>();
            damage = enemyAttributes.GetDamage();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Cannon.CannonTakeDamage>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
