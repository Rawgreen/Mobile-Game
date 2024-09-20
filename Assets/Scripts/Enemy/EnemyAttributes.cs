using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttributes : MonoBehaviour
    {
        [SerializeField, Range(1, 1000)] private int maxHealth = 10;
        [SerializeField] private int tempHealth;
        [SerializeField, Range(0.1f, 2.0f)] private float moveSpeed = 0.33f;
        [SerializeField, Range(1, 5)] private int damage = 2;
        [SerializeField, Range(1, 1000)] private int pointsWorth = 10;
        [SerializeField, Range(1, 10000)] private int goldsWorth = 1;

        public void SetTempHealth(int health)
        {
            tempHealth = health;
        }

        public int GetTempHealth()
        {
            return tempHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(int newHealth)
        {
            maxHealth = newHealth;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void SetDamage(int newDamage)
        {
            damage = newDamage;
        }

        public int GetPointsWorth()
        {
            return pointsWorth;
        }

        public int GetGoldsWorth()
        {
            return goldsWorth;
        }
    }
}
