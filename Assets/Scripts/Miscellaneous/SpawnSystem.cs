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

        [SerializeField] private bool isSpawning = true;
        [SerializeField, Range(0.1f, 5f)] private float spawnRate = 2f;
        [SerializeField] private GameObject[] enemyPrefabs;

        [SerializeField] private int maxEnemiesAlive = 50;
        [SerializeField] private int enemiesAlive = 0;
        [SerializeField] private int killTracker = 0;
        [SerializeField] private int totalGoldsEarned = 0;
        [SerializeField] private int allTimeScore = 0;
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int currentGolds = 0;


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
            enemiesAlive += 1;
            if (enemyCounts.ContainsKey(tag))
            {
                enemyCounts[tag] += 1;
            }
            else
            {
                enemyCounts[tag] = 1;
            }
        }

        public void RemoveEnemyAlive(string tag, int pointsWorth, int goldWorth)
        {
            KillTrackerUp();
            AddGolds(goldWorth);
            AddScore(pointsWorth);

            enemiesAlive -= 1;
            if (enemyCounts.ContainsKey(tag))
            {
                enemyCounts[tag] -= 1;
            }
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

        public void KillTrackerUp()
        {
            killTracker += 1;
        }

        public int GetKillTracker()
        {
            return killTracker;
        }

        public void AddGolds(int goldsWorth)
        {
            currentGolds += goldsWorth;
            totalGoldsEarned += goldsWorth;
        }

        public int GetTotalGoldsEarned()
        {
            return totalGoldsEarned;
        }

        public int GetScore()
        {
            return currentScore;
        }

        public int GetAllTimeScore()
        {
            return allTimeScore;
        }

        public void AddScore(int point)
        {
            currentScore += point;
            AllTimeScoreTracker();
        }

        public void AllTimeScoreTracker()
        {

            if (currentScore >= allTimeScore)
            {
                allTimeScore = currentScore;
            }
        }
    }
}
