using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Miscellaneous
{
    public class SpawnSystem : MonoBehaviour
    {
        // Singleton pattern
        public static SpawnSystem Instance { get; private set; }

        private Dictionary<string, int> enemyCounts = new Dictionary<string, int>();
        private Miscellaneous.GameManager gameManager;

        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private bool isSpawning = true;
        [SerializeField, Range(0.1f, 5f)] private float spawnRate = 2f;
        [SerializeField] private int maxEnemiesAlive = 50;
        [SerializeField] private int enemiesAlive = 0;

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
            gameManager = GameManager.Instance;
            StartCoroutine(Spawner());
        }

        private void Update()
        {
            if (enemiesAlive >= maxEnemiesAlive)
            {
                isSpawning = false;
            } 
            else
            {
                isSpawning = true;
            }
        }

        private IEnumerator Spawner()
        {
            WaitForSeconds wait = new WaitForSeconds(spawnRate);

            while (isSpawning)
            {
                GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                GameObject spawnedEnemy = Instantiate(randomEnemy, transform.position, Quaternion.identity);
                AddEnemyAlive(spawnedEnemy.tag);
                yield return wait;
            }
        }

        public void AddEnemyAlive(string tag)
        {
            enemiesAlive++;
            if (enemyCounts.ContainsKey(tag))
            {
                enemyCounts[tag]++;
            }
            else
            {
                enemyCounts[tag] = 1;
            }
        }

        public void RemoveEnemyAlive(string tag, int goldsWorth, int pointsWorth)
        {
            enemiesAlive--;
            if (enemyCounts.ContainsKey(tag))
            {
                enemyCounts[tag]--;
            }

            gameManager.KillTrackerUp();
            gameManager.EarnGolds(goldsWorth);
            gameManager.ScoreUp(pointsWorth);
        }

        public int GetEnemiesAlive()
        {
            return enemiesAlive;
        }

        public int GetEnemyCountByTag(string tag)
        {
            if (enemyCounts.ContainsKey(tag))
            {
                return enemyCounts[tag];
            }
            return 0;
        }
    }
}
