using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonTakeDamage : MonoBehaviour
    {
        private GameManager gameManager;
        private CannonAttributes cannonAttributes;
        private int maxHealth;
        private int tempHealth;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            cannonAttributes = GetComponent<CannonAttributes>();
            // initial maxHealth
            maxHealth = cannonAttributes.GetMaxHealth();
            tempHealth = maxHealth;
            cannonAttributes.SetTempHealth(tempHealth);
        }

        public void TakeDamage(int damage)
        {
            tempHealth -= damage;
            cannonAttributes.SetTempHealth(tempHealth);
            if (tempHealth <= 0)
            {
                gameObject.SetActive(false);
                gameManager.GameOver();
            }
        }
    }
}