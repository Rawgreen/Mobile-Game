using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonAttributes : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int maxHealth = 10;
        [SerializeField] private int tempHealth;
        [SerializeField, Range(1, 1000)] private int damage = 10;
        [SerializeField, Range(1f, 25f)] private float projectileSpeed = 5f;
        [SerializeField, Range(1, 10)] private float radius = 4f;
        [SerializeField, Range(0.1f, 10)] private float shootingSpeed = 1f;
        [SerializeField, Range(45, 720)] private int circleCorners = 360;
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private LayerMask enemyLayer;

        public int GetTempHealth()
        {
            return tempHealth;
        }

        public void SetTempHealth(int health)
        {
            tempHealth = health;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(int newHealth)
        {
            maxHealth = newHealth;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void SetDamage(int newDamage)
        {
            damage = newDamage;
        }

        public float GetRadius()
        {
            return radius;
        }

        public void SetRadius(float newRadius)
        {
            radius = newRadius;
        }

        public float GetShootingSpeed()
        {
            return shootingSpeed;
        }

        public void SetShootingSpeed(float newShootingSpeed)
        {
            shootingSpeed = newShootingSpeed;
        }

        public int GetCircleCorners()
        {
            return circleCorners;
        }

        public void SetCircleCorners(int newCircleCorners)
        {
            circleCorners = newCircleCorners;
        }

        public float GetProjectileSpeed()
        {
            return projectileSpeed;
        }

        public void SetProjectileSpeed(float newProjectileSpeed)
        {
            projectileSpeed = newProjectileSpeed;
        }

        public GameObject GetProjectilePrefab()
        {
            return projectilePrefab;
        }

        public Transform GetShootingPoint()
        {
            return shootingPoint;
        }

        public LayerMask GetEnemyLayer()
        {
            return enemyLayer;
        }
    }
}