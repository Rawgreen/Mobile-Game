using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonManager : MonoBehaviour
    {
        private float radius;

        private LayerMask enemyLayer;
        private GameObject closestEnemy;

        private void Start()
        {
            radius = GetComponent<CannonAttributes>().GetRadius();
            enemyLayer = GetComponent<CannonAttributes>().GetEnemyLayer();
        }

        private void Update()
        {
            closestEnemy = CalculateClosestEnemy();
        }

        public GameObject GetClosestEnemy()
        {
            return closestEnemy;
        }

        private GameObject CalculateClosestEnemy()
        {
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, radius / 2.5f, enemyLayer);
            if (detectedEnemies.Length > 0)
            {
                float maxDistance = 10000f;
                foreach (Collider2D enemy in detectedEnemies)
                {
                    float currentDistance = CalculateDistance(enemy.transform);
                    if (currentDistance < maxDistance)
                    {
                        maxDistance = currentDistance;
                        closestEnemy = enemy.gameObject;
                    }
                }
                return closestEnemy;
            }
            else
            {
                return null;
            }
        }

        private float CalculateDistance(Transform enemy)
        {
            return Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - gameObject.transform.position.x, 2) +
                              Mathf.Pow(enemy.transform.position.y - gameObject.transform.position.y, 2));
        }
    }
}