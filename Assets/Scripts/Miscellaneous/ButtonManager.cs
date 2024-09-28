using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private CannonStats cannonStats;
    private GameObject achievementContainer;
    [SerializeField] private GameObject achievementPrefab;
    [SerializeField] private GameObject achievementScrollFrame;

    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    private Dictionary<string, TextMeshProUGUI> buttonTexts = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, float> upgradeAmounts = new Dictionary<string, float>();

    private void Start()
    {
        cannonStats = CannonManager.Instance.GetCannonStats();
        achievementContainer = FindChildRecursive(achievementScrollFrame.transform, "Content");

        InitializeButton("DamageUpgrade", 1f, 5f, true);
        InitializeButton("HealthUpgrade", 1f, 5f, true);
        InitializeButton("RadiusUpgrade", 0.05f, 0.1f, true);
        InitializeButton("ProjectileVelocityUpgrade", 0.1f, 1.0f, true);
        InitializeButton("ShootingRateUpgrade", 0.01f, 0.05f, true);
        InitializeButton("Achievements", 0, 0, true);
    }

    private float GenerateRandomValue(float from, float to)
    {
        float randomValue = Random.Range(from, to);
        return Mathf.Round(randomValue * 100f) / 100f;
    }

    private void UpdateValueText(string buttonName, TextMeshProUGUI buttonText, float value)
    {
        if (buttonName == "DamageUpgrade" || buttonName == "HealthUpgrade")
        {
            buttonText.text = $"+{(int)value}";
        }
        else
        {
            buttonText.text = $"+{value:F2}";
        }
    }

    private TextMeshProUGUI GetTextComponent(Button button)
    {
        // Try to find the TextMeshProUGUI component in the button's children
        TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
        return textComponent;
    }

    private void InitializeButton(string buttonName, float minValue = 0.0f, float maxValue = 0.0f, bool isUpgrade = false)
    {
        Button button = GameObject.Find(buttonName)?.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError($"Button '{buttonName}' not found.");
            return;
        }

        buttons[buttonName] = button;

        TextMeshProUGUI buttonText = GetTextComponent(button);
        if (buttonText != null)
        {
            buttonTexts[buttonName] = buttonText;

            float upgradeAmount = GenerateRandomValue(minValue, maxValue);
            upgradeAmounts[buttonName] = upgradeAmount;

            UpdateValueText(buttonName, buttonText, upgradeAmount);
        }
        else
        {
            Debug.LogWarning($"Button '{buttonName}' does not have a text component.");
        }
    }

    // Recursive method to find a child object by name
    private GameObject FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }

            GameObject result = FindChildRecursive(child, childName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public void ShowAchievements()
    {
        bool isActive = !achievementScrollFrame.activeSelf;
        achievementScrollFrame.SetActive(isActive);
        if (isActive)
        {
            // Pause the game
            Time.timeScale = 0;
            PopulateAchievements();
        }
        else
        {
            // Resume the game
            Time.timeScale = 1;
        }
    }

    private void PopulateAchievements()
    {
        // Clear existing achievements
        foreach (Transform child in achievementContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Get achievements from AchievementManager
        List<Achievement> achievements = AchievementManager.Instance.GetAchievements();

        // Instantiate each achievement
        foreach (Achievement achievement in achievements)
        {
            GameObject achievementInstance = Instantiate(achievementPrefab, achievementContainer.transform);
            GameObject iconObject = achievementInstance.transform.Find("Icon").gameObject;
            GameObject lockedIcon = achievementInstance.transform.Find("LockedIcon").gameObject;
            GameObject unlockedObject = achievementInstance.transform.Find("UnlockedIcon").gameObject;
            GameObject nameObject = achievementInstance.transform.Find("AchievementName").gameObject;
            GameObject descriptionObject = achievementInstance.transform.Find("AchievementDescription").gameObject;

            if (iconObject != null)
            {
                iconObject.GetComponent<Image>().sprite = achievement.icon;
            }

            if (lockedIcon != null && !achievement.isUnlocked)
            {
                lockedIcon.SetActive(true);
            }
            else if (lockedIcon != null)
            {
                lockedIcon.SetActive(false);
            }

            if (unlockedObject != null && achievement.isUnlocked)
            {
                unlockedObject.SetActive(true);
            }
            else if (unlockedObject != null)
            {
                unlockedObject.SetActive(false);
            }

            if (nameObject != null)
            {
                nameObject.GetComponent<TextMeshProUGUI>().text = achievement.title;
            }

            if (descriptionObject != null)
            {
                descriptionObject.GetComponent<TextMeshProUGUI>().text = achievement.description;
            }
        }
    }

    public void UpgradeDamage()
    {
        UpgradeStat("DamageUpgrade", () => cannonStats.SetDamage(cannonStats.GetDamage() + (int)upgradeAmounts["DamageUpgrade"]));
    }

    public void UpgradeHealth()
    {
        UpgradeStat("HealthUpgrade", () =>
        {
            cannonStats.SetTempHealth(cannonStats.GetTempHealth() + (int)upgradeAmounts["HealthUpgrade"]);
            cannonStats.SetMaxHealth(cannonStats.GetMaxHealth() + (int)upgradeAmounts["HealthUpgrade"]);
        });
    }

    public void UpgradeProjectileSpeed()
    {
        UpgradeStat("ProjectileVelocityUpgrade", () => cannonStats.SetProjectileSpeed(cannonStats.GetProjectileSpeed() + upgradeAmounts["ProjectileVelocityUpgrade"]));
    }

    public void UpgradeRadius()
    {
        UpgradeStat("RadiusUpgrade", () => cannonStats.SetRadius(cannonStats.GetRadius() + upgradeAmounts["RadiusUpgrade"]));
    }

    public void UpgradeShootingSpeed()
    {
        // => is a lambda expression. It is a way to pass a function as a parameter.
        // () means no parameters used in the lambda expression.
        UpgradeStat("ShootingRateUpgrade", () => cannonStats.SetShootingSpeed(cannonStats.GetShootingSpeed() + upgradeAmounts["ShootingRateUpgrade"]));
    }

    private void UpgradeStat(string buttonName, System.Action upgradeAction)
    {
        // Executes the lambda expression that passed into it.
        upgradeAction.Invoke();

        Debug.Log($"{buttonName} upgraded by {upgradeAmounts[buttonName]}. New value: {GetStatValue(buttonName)}");
        float newUpgradeAmount = GenerateRandomValue(GetMinValue(buttonName), GetMaxValue(buttonName));
        upgradeAmounts[buttonName] = newUpgradeAmount;
        UpdateValueText(buttonName, buttonTexts[buttonName], newUpgradeAmount);
    }

    private float GetStatValue(string buttonName)
    {
        switch (buttonName)
        {
            case "DamageUpgrade": return cannonStats.GetDamage();
            case "HealthUpgrade": return cannonStats.GetTempHealth();
            case "ProjectileVelocityUpgrade": return cannonStats.GetProjectileSpeed();
            case "RadiusUpgrade": return cannonStats.GetRadius();
            case "ShootingRateUpgrade": return cannonStats.GetShootingSpeed();
            default: return 0;
        }
    }

    private float GetMinValue(string buttonName)
    {
        switch (buttonName)
        {
            case "DamageUpgrade":
            case "HealthUpgrade": return 1f;
            case "RadiusUpgrade": return 0.05f;
            case "ProjectileVelocityUpgrade": return 0.1f;
            case "ShootingRateUpgrade": return 0.01f;
            default: return 0;
        }
    }

    private float GetMaxValue(string buttonName)
    {
        switch (buttonName)
        {
            case "DamageUpgrade":
            case "HealthUpgrade": return 2f;
            case "RadiusUpgrade": return 0.1f;
            case "ProjectileVelocityUpgrade": return 1.0f;
            case "ShootingRateUpgrade": return 0.05f;
            default: return 0;
        }
    }
}
