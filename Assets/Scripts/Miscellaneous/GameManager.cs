using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        //[SerializeField] private List<AchievementManager> achievements;

        [SerializeField] private int golds = 0;
        [SerializeField] private int score = 0;
        [SerializeField] private int enemiesKilled = 0;

        [SerializeField] private int totalEnemiesKilled = 0;
        [SerializeField] private int totalGoldsEarned = 0;
        [SerializeField] private int AllTimeScore = 0;


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

        //private void Start()
        //{
        //    InitializeAchievements();
        //}

        //private void Update()
        //{
        //    CheckAchievementCompletion();
        //}

        public void GameOver()
        {
            Debug.Log("Game Over");
        }

        //private void CheckAchievementCompletion()
        //{
        //    if (achievements != null)
        //    {
        //        foreach (AchievementManager achievement in achievements)
        //        {
        //            achievement.UnlockAchievement();
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        
        //private bool AchievementUnlocked(string achievementName)
        //{
        //    bool result = false;
            
        //    if (achievements == null)
        //    {
        //        return result;
        //    }

        //    AchievementManager[] achievementArray = achievements.ToArray();
        //    AchievementManager a = Array.Find(achievementArray, element => achievementName == element.achievementName);

        //    if (a == null)
        //    {
        //        return result;
        //    }
        //    result = a.isUnlocked;
        //    return result;
        //}

        //private void InitializeAchievements()
        //{
        //    if (achievements != null)
        //    {
        //        return;
        //    } 
        //    else
        //    {
        //        //TODO: Add more achievements
        //        achievements = new List<AchievementManager>();
        //        // => is a lambda expression
        //        // kill related achievements
        //        achievements.Add(new AchievementManager("Kill Achievement Test", "TEST", (object obj) => totalEnemiesKilled == 1));
        //        achievements.Add(new AchievementManager("First Blood", "Defeat 100 enemies in total across all matches.", (object obj) => totalEnemiesKilled == 100));
        //        achievements.Add(new AchievementManager("Mass Exterminator", "Rack up a total of 1,000 enemy kills and leave no survivors.", (object obj) => totalEnemiesKilled >= 1000));
        //        achievements.Add(new AchievementManager("Army Annihilator", "Reach 5,000 total kills and crush waves of enemies with precision.", (object obj) => totalEnemiesKilled >= 5000));
        //        achievements.Add(new AchievementManager("Warlord of Destruction", "Amass a total kill count of 10,000 enemies and show your dominance in battle.", (object obj) => totalEnemiesKilled >= 10000));
        //        achievements.Add(new AchievementManager("Eternal Slayer", "Eliminate 50,000 enemies in total, proving you're an unstoppable force in the tower defense world.", (object obj) => totalEnemiesKilled >= 50000));
        //        // score related achievements
        //        achievements.Add(new AchievementManager("Score Achievement Test", "TEST", (object obj) => AllTimeScore >= 10));
        //        achievements.Add(new AchievementManager("Rising Star", "Set your first all-time high score by surpassing 2,000 points.", (object obj) => AllTimeScore >= 2000));
        //        achievements.Add(new AchievementManager("Hall of Fame", "Earn a place in the leaderboard by reaching an all-time high score of 10,000.", (object obj) => AllTimeScore >= 10000));
        //        achievements.Add(new AchievementManager("Legendary Defender", "Break the 25,000 all-time high score mark and secure your legacy as a top defender.", (object obj) => AllTimeScore >= 25000));
        //        achievements.Add(new AchievementManager("Immortal Champion", "Achieve an all-time high score of 50,000 and show the world your unbeatable strategy.", (object obj) => AllTimeScore >= 50000));
        //        achievements.Add(new AchievementManager("Cosmic Guardian", "Set an unstoppable all-time high score of 100,000 and become a true tower defense legend.", (object obj) => AllTimeScore >= 100000));
        //        // gold related achievements
        //    }
        //}

        public void KillTrackerUp()
        {
            enemiesKilled++;
            totalEnemiesKilled++;
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
            if (score >= AllTimeScore)
            {
               AllTimeScore = score;
            }
        }

        public int GetScore()
        {
            return score;
        }

        public int GetAllTimeScore()
        {
            return AllTimeScore;
        }
    }
}
