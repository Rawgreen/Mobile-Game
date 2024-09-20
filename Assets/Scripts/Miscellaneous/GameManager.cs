using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score = 0;
    [SerializeField] private int killTracker = 0;
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

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void AddGolds(int goldsWorth)
    {
        golds += goldsWorth;
        totalGoldsEarned += goldsWorth;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int point)
    {
        score += point;
    }

    public int GetKillTracker()
    {
        return killTracker;
    }

    public void KillTrackerUp() 
    {
        killTracker += 1;
    }
}