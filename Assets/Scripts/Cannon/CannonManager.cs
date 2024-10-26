using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private ButtonManager buttonManager;
        private GameObject cannonObject;

        [Header("Reset Values")]
        [SerializeField] private int alternativeHealth = 10;
        [SerializeField] private int alternativeDamage = 10;
        [SerializeField] private int alternativeCircleCorners = 720;
        [SerializeField] private float alternativeProjectileSpeed = 3f;
        [SerializeField] private float alternativeRadius = 4f;
        [SerializeField] private float alternativeShootingSpeed = 1f;
        [SerializeField] private GameObject alternativeProjectilePrefab;

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
        }

        private void Start()
        {
            //cannonStats.ResetValuesToInitial();
            cannonObject = GameObject.Find("Cannon");

            // reset the stats to the default values after each playthrough
            cannonStats.ResetStats(alternativeHealth, alternativeDamage, alternativeCircleCorners, alternativeProjectileSpeed, alternativeRadius, alternativeShootingSpeed, alternativeProjectilePrefab);
            buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
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