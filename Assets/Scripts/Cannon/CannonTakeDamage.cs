using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonTakeDamage : MonoBehaviour
    {
        private Miscellaneous.GameManager gameManager;
        private CannonStats cannonStats;
        private int maxHealth;
        private int tempHealth;

        private void Start()
        {
            gameManager = Miscellaneous.GameManager.Instance;
            cannonStats = CannonManager.Instance.GetCannonStats();
            // initial maxHealth
            maxHealth = cannonStats.GetMaxHealth();
            tempHealth = maxHealth;
            cannonStats.SetTempHealth(tempHealth);
        }

        private void Update()
        {
            cannonStats = CannonManager.Instance.GetCannonStats();
        }

        public void TakeDamage(int damage)
        {
            tempHealth -= damage;
            cannonStats.SetTempHealth(tempHealth);
            if (tempHealth <= 0)
            {
                gameObject.SetActive(false);
                gameManager.GameOver();
            }
        }
    }
}