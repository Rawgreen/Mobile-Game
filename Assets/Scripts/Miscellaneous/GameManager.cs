using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private int score = 0;
        [SerializeField] private int allTimeScore = 0;
        [SerializeField] private int enemiesKilled = 0;
        [SerializeField] private int totalEnemiesKilled = 0;
        [SerializeField] private int golds = 0;
        [SerializeField] private int totalGoldsEarned = 0;

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
            //TODO: remove after testing
            // Resets achievements every time the game starts
            AchievementManager.Instance.ResetAchievements();
            AchievementManager.Instance.OnAchievementUnlocked += OnAchievementUnlocked;
        }

        private void OnDestroy()
        {
            AchievementManager.Instance.OnAchievementUnlocked -= OnAchievementUnlocked;
        }

        private void OnAchievementUnlocked(Achievement achievement)
        {
            Debug.Log($"{achievement.title}: {achievement.description}");
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
        }

        public void KillTrackerUp()
        {
            enemiesKilled++;
            totalEnemiesKilled++;
            CheckKillAchievements();
        }

        private void CheckKillAchievements()
        {
            if (totalEnemiesKilled >= 50000)
            {
                AchievementManager.Instance.UnlockAchievement("Endless Slayer");
            }
            else if (totalEnemiesKilled >= 10000)
            {
                AchievementManager.Instance.UnlockAchievement("Warlord of Destruction");
            }
            else if (totalEnemiesKilled >= 5000)
            {
                AchievementManager.Instance.UnlockAchievement("Army Annihilator");
            }
            else if (totalEnemiesKilled >= 1000)
            {
                AchievementManager.Instance.UnlockAchievement("Mass Exterminator");
            }
            else if (totalEnemiesKilled >= 100)
            {
                AchievementManager.Instance.UnlockAchievement("First Blood");
            }
            else if (enemiesKilled >= 1)
            {
                AchievementManager.Instance.UnlockAchievement("Kill1");
            }
        }

        public int GetKillTracker()
        {
            return enemiesKilled;
        }

        public int GetTotalKillTracker()
        {
            return totalEnemiesKilled;
        }

        public void EarnGolds(int goldsWorth)
        {
            golds += goldsWorth;
            totalGoldsEarned += goldsWorth;
        }

        public int GetGolds()
        {
            return golds;
        }

        public int GetTotalGolds()
        {
            return totalGoldsEarned;
        }

        public void ScoreUp(int scoreWorth)
        {
            score += scoreWorth;
            if (score >= allTimeScore)
            {
                allTimeScore = score;
                CheckScoreAchievements();
            }
        }

        private void CheckScoreAchievements()
        {
            if (allTimeScore >= 100000)
            {
                AchievementManager.Instance.UnlockAchievement("Eternal Guardian");
            }
            else if (allTimeScore >= 50000)
            {
                AchievementManager.Instance.UnlockAchievement("Immortal Champion");
            }
            else if (allTimeScore >= 25000)
            {
                AchievementManager.Instance.UnlockAchievement("Legendary Defender");
            }
            else if (allTimeScore >= 10000)
            {
                AchievementManager.Instance.UnlockAchievement("Hall of Fame");
            }
            else if (allTimeScore >= 2000)
            {
                AchievementManager.Instance.UnlockAchievement("Rising Star");
            }
            else if (allTimeScore >= 1)
            {
                AchievementManager.Instance.UnlockAchievement("Score1");
            }
        }

        public int GetScore()
        {
            return score;
        }

        public int GetAllTimeScore()
        {
            return allTimeScore;
        }
    }
}
