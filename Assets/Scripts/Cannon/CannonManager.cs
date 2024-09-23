using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonManager : MonoBehaviour
    {
        // Singleton pattern
        public static CannonManager Instance { get; private set; }
        [SerializeField] private CannonStats cannonStats;

        private float radius;
        private LayerMask enemyLayer;
        private GameObject closestEnemy;
        private GameObject cannonObject;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Get cannon object and assign its position to cannon manager
            // so that it can be used to calculate the closest enemy without any problems.
            cannonObject = GameObject.Find("Cannon");
        }

        private void Start()
        {
            radius = cannonStats.GetRadius();
            enemyLayer = cannonStats.GetEnemyLayer();
        }

        private void Update()
        {
            closestEnemy = CalculateClosestEnemy();
        }

        public CannonStats GetCannonStats()
        {
            return cannonStats;
        }

        public GameObject GetClosestEnemy()
        {
            return closestEnemy;
        }

        private GameObject CalculateClosestEnemy()
        {
            GameObject closestEnemy = null;
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(cannonObject.transform.position, cannonStats.GetRadius() / 2.5f, cannonStats.GetEnemyLayer());
            if (detectedEnemies.Length > 0)
            {
                float maxDistance = 10000f;
                foreach (Collider2D enemy in detectedEnemies)
                {
                    float currentDistance = Miscellaneous.HelperFunctions.CalculateDistance(gameObject.transform, enemy.transform);
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
    }
}