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
        private SpawnSystem spawnSystem;

        private void Awake()
        {
            spawnSystem = SpawnSystem.Instance;
            pointsWorth = spawnSystem.GetEnemyStats().GetPointsWorth();
            goldsWorth = spawnSystem.GetEnemyStats().GetGoldsWorth();
            damage = spawnSystem.GetEnemyStats().GetDamage();

            // initial max maxHealth
            maxHealth = spawnSystem.GetEnemyStats().GetMaxHealth();
            tempHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            tempHealth -= damage;
            spawnSystem.GetEnemyStats().SetTempHealth(tempHealth);
            if (tempHealth <= 0)
            {
                Destroy(gameObject);
                spawnSystem.RemoveEnemyAlive(gameObject.tag);
                GameManager.Instance.KillTrackerUp(pointsWorth, goldsWorth);
            }
        }
    }
}
