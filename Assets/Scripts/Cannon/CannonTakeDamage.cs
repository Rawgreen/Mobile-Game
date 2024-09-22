using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonTakeDamage : MonoBehaviour
    {
        private Miscellaneous.GameManager gameManager;
        [SerializeField] private CannonStats cannonStats;
        private int maxHealth;
        private int tempHealth;

        private void Awake()
        {
            gameManager = Miscellaneous.GameManager.Instance;
            // initial maxHealth
            maxHealth = cannonStats.GetMaxHealth();
            tempHealth = maxHealth;
            cannonStats.SetTempHealth(tempHealth);
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