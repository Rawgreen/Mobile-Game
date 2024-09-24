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
            //TODO: remove after testing
            //Debug.Log($"{achievement.title}: {achievement.description}");
            return;
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
        }

        public void KillTrackerUp(int pointsWorth, int goldsWorth)
        {
            enemiesKilled++;
            totalEnemiesKilled++;
            golds += goldsWorth;
            totalGoldsEarned += goldsWorth;
            score += pointsWorth;
            if (score >= allTimeScore)
            {
                allTimeScore = score;
            }
            CheckKillAchievements();
            CheckGoldAchievements();
            CheckScoreAchievements();
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
            //TODO: remove after testing
            //else if (totalEnemiesKilled >= 1)
            //{
            //    AchievementManager.Instance.UnlockAchievement("Kill1");
            //}
        }

        private void CheckGoldAchievements()
        {
            if (totalGoldsEarned >= 500000)
            {
                AchievementManager.Instance.UnlockAchievement("Master of Wealth");
            }
            else if (totalGoldsEarned >= 100000)
            {
                AchievementManager.Instance.UnlockAchievement("King of Riches");
            }
            else if (totalGoldsEarned >= 50000)
            {
                AchievementManager.Instance.UnlockAchievement("Fortune Hoarder");
            }
            else if (totalGoldsEarned >= 10000)
            {
                AchievementManager.Instance.UnlockAchievement("Golden Strategist");
            }
            else if (totalGoldsEarned >= 1000)
            {
                AchievementManager.Instance.UnlockAchievement("Treasure Seeker");
            }
            //TODO: remove after testing
            //else if (totalGoldsEarned >= 1)
            //{
            //    AchievementManager.Instance.UnlockAchievement("Gold1");
            //}
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
            //TODO: remove after testing
            //else if (allTimeScore >= 1)
            //{
            //    AchievementManager.Instance.UnlockAchievement("Score1");
            //}
        }
    }
}
