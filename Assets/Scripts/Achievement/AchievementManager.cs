using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private string description;
    private Predicate<object> requirement;

    public bool isUnlocked;
    public string achievementName;

    public AchievementManager(string achievementName, string description, Predicate<object> requirement)
    {
        this.achievementName = achievementName;
        this.description = description;
        this.requirement = requirement;
    }

    public void UnlockAchievement()
    {
        if (RequirementsMet())
        {
            isUnlocked = true;
            Debug.Log($"{this.achievementName}: {this.description}");
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}

