using Miscellaneous;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyTakeDamage : MonoBehaviour
    {
        private int maxHealth;
        private int tempHealth;
        private int damage;
        private int pointsWorth;
        private int goldsWorth;
        private EnemyAttributes enemyAttributes;
        private SpawnSystem spawnSystem;

        private void Awake()
        {
            spawnSystem = SpawnSystem.Instance;
            enemyAttributes = GetComponent<EnemyAttributes>();
            pointsWorth = enemyAttributes.GetPointsWorth();
            goldsWorth = enemyAttributes.GetGoldsWorth();
            damage = enemyAttributes.GetDamage();

            // initial max maxHealth
            maxHealth = enemyAttributes.GetMaxHealth();
            tempHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            tempHealth -= damage;
            enemyAttributes.SetTempHealth(tempHealth);
            if (tempHealth <= 0)
            {
                Destroy(gameObject);
                spawnSystem.RemoveEnemyAlive(gameObject.tag, goldsWorth, pointsWorth);
            }
        }
    }
}
