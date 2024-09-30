using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cannon;
using Miscellaneous;
using Random = UnityEngine.Random;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    private CannonStats cannonStats;

    private GameObject achievementContainer;
    [SerializeField] private GameObject achievementPrefab;
    [SerializeField] private GameObject achievementScrollFrame;
    [SerializeField] private TextMeshProUGUI coinsLeftText;
    [SerializeField] private TextMeshProUGUI healthLeftText;

    public Dictionary<string, int> upgradeCosts = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> UpgradeCostTextOnButtons = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    private Dictionary<string, float> upgradeAmounts = new Dictionary<string, float>();

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
        cannonStats = CannonManager.Instance.GetCannonStats();
        achievementContainer = FindChildRecursive(achievementScrollFrame.transform, "Content");

        InitializeButton("DamageUpgrade", 10, 1f, 5f, true);
        InitializeButton("HealthUpgrade", 10, 1f, 5f, true);
        InitializeButton("RadiusUpgrade", 10, 0.05f, 0.1f, true);
        InitializeButton("ProjectileVelocityUpgrade", 10, 0.1f, 1.0f, true);
        InitializeButton("ShootingRateUpgrade", 10, 0.01f, 0.05f, true);
        InitializeButton("Achievements", 0, 0, 0, true);

        UpdateCoinsLeftText();
    }

    public void UpgradeDamage()
    {
        GameManager.Instance.MakeUpgrade("DamageUpgrade");
        UpgradeStat("DamageUpgrade", () => cannonStats.SetDamage(cannonStats.GetDamage() + (int)upgradeAmounts["DamageUpgrade"]));
    }

    public void UpgradeHealth()
    {
        GameManager.Instance.MakeUpgrade("HealthUpgrade");
        UpgradeStat("HealthUpgrade", () =>
        {
            int newHealth = cannonStats.GetTempHealth() + (int)upgradeAmounts["HealthUpgrade"];
            cannonStats.SetTempHealth(newHealth);
            cannonStats.SetMaxHealth(cannonStats.GetMaxHealth() + (int)upgradeAmounts["HealthUpgrade"]);
            UpdateHealthLeft(cannonStats.GetTempHealth());
        });
    }

    public void UpgradeProjectileSpeed()
    {
        GameManager.Instance.MakeUpgrade("ProjectileVelocityUpgrade");
        UpgradeStat("ProjectileVelocityUpgrade", () => cannonStats.SetProjectileSpeed(cannonStats.GetProjectileSpeed() + upgradeAmounts["ProjectileVelocityUpgrade"]));
    }

    public void UpgradeRadius()
    {
        GameManager.Instance.MakeUpgrade("RadiusUpgrade");
        UpgradeStat("RadiusUpgrade", () => cannonStats.SetRadius(cannonStats.GetRadius() + upgradeAmounts["RadiusUpgrade"]));
    }

    public void UpgradeShootingSpeed()
    {
        GameManager.Instance.MakeUpgrade("ShootingRateUpgrade");
        UpgradeStat("ShootingRateUpgrade", () => cannonStats.SetShootingSpeed(cannonStats.GetShootingSpeed() + upgradeAmounts["ShootingRateUpgrade"]));
    }

    public void UpdateButtonStatus()
    {
        int currentGolds = GameManager.Instance.GetGolds();

        foreach (var buttonEntry in buttons)
        {
            if (buttonEntry.Key == "Achievements")
            {
                continue;
            }
            string buttonName = buttonEntry.Key;
            Button button = buttonEntry.Value;
            int upgradeCost = upgradeCosts[buttonName];

            button.interactable = currentGolds >= upgradeCost;
        }
    }

    public void UpdateCoinsLeftText()
    {
        int currentGolds = GameManager.Instance.GetGolds();
        if (currentGolds >= 1000000)
        {
            coinsLeftText.text = $"{currentGolds / 1000000:F1}M";
        }
        else if (currentGolds >= 1000)
        {
            coinsLeftText.text = $"{currentGolds / 1000:F1}K";
        }
        else
        {
            coinsLeftText.text = $"{currentGolds}";
            coinsLeftText.text = currentGolds.ToString();
        }
    }

    private void UpdateHealthLeft(int currentHealth)
    {
        if (currentHealth >= 1000000)
        {
            healthLeftText.text = $"{currentHealth / 1000000:F1}M";
        }
        else if (currentHealth >= 1000)
        {
            healthLeftText.text = $"{currentHealth / 1000:F1}K";
        }
        else
        {
            healthLeftText.text = currentHealth.ToString();
        }
    }

    private void UpgradeStat(string buttonName, System.Action upgradeAction)
    {
        upgradeAction.Invoke();
        bool isIntegerUpgrade = buttonName == "DamageUpgrade" || buttonName == "HealthUpgrade";
        float newUpgradeAmount = GenerateRandomValue(GetMinValue(buttonName), GetMaxValue(buttonName), isIntegerUpgrade);
        upgradeAmounts[buttonName] = newUpgradeAmount;

        TextMeshProUGUI upgradeCostText = UpgradeCostTextOnButtons[buttonName];
        UpdateValueText(upgradeCostText, newUpgradeAmount);

        // Update the cost text
        if (upgradeCosts.ContainsKey(buttonName))
        {
            TextMeshProUGUI costText = GetButtonTextComponent(buttons[buttonName], "UpgradeCost");
            UpdateValueText(costText, upgradeCosts[buttonName]); // Access the Item1 of the tuple
        }
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

    private void ShowAchievements()
    {
        bool isActive = !achievementScrollFrame.activeSelf;
        achievementScrollFrame.SetActive(isActive);
        if (isActive)
        {
            Time.timeScale = 0;
            PopulateAchievements();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void PopulateAchievements()
    {
        foreach (Transform child in achievementContainer.transform)
        {
            Destroy(child.gameObject);
        }

        List<Achievement> achievements = AchievementManager.Instance.GetAchievements();

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

    private void UpdateValueText(TextMeshProUGUI itemText, float value)
    {
        if (itemText.name == "UpgradeCost")
        {
            if (value >= 1000000)
            {
                itemText.text = $"{value / 1000000:F1}M";
            }
            else if (value >= 1000)
            {
                itemText.text = $"{value / 1000:F1}K";
            }
            else
            {
                itemText.text = $"{value}";
            }
        }
        else
        {
            itemText.text = $"+{value:F2}";
        }
    }

    private TextMeshProUGUI GetButtonTextComponent(Button button, string componentName)
    {
        TextMeshProUGUI[] components = button.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI component in components)
        {
            if (component.name == componentName)
            {
                return component;
            }
        }
        return null;
    }

    private void InitializeButton(string buttonName, int upgradeCost, float minValue = 0.0f, float maxValue = 0.0f, bool isUpgrade = false)
    {
        Button button = GameObject.Find(buttonName)?.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError($"Button '{buttonName}' not found.");
            return;
        }

        buttons[buttonName] = button;

        TextMeshProUGUI upgradeCostText = GetButtonTextComponent(button, "UpgradeValue");
        if (upgradeCostText != null)
        {
            UpgradeCostTextOnButtons[buttonName] = upgradeCostText;

            bool isIntegerUpgrade = buttonName == "DamageUpgrade" || buttonName == "HealthUpgrade";
            float upgradeAmount = GenerateRandomValue(minValue, maxValue, isIntegerUpgrade);
            upgradeAmounts[buttonName] = upgradeAmount;

            UpdateValueText(upgradeCostText, upgradeAmount);
        }

        TextMeshProUGUI costText = GetButtonTextComponent(button, "UpgradeCost");
        if (upgradeCost != 0)
        {
            upgradeCosts[buttonName] = upgradeCost; // Assign a tuple (int, bool)
            UpdateValueText(costText, upgradeCosts[buttonName]); // Access the Item1 of the tuple
        }
    }

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

    private float GenerateRandomValue(float from, float to, bool isInteger = false)
    {
        if (isInteger)
        {
            return Random.Range(1, 3);
        }
        float randomValue = Random.Range(from, to);
        return Mathf.Round(randomValue * 100f) / 100f;
    }

    public int GetUpgradeCost(string buttonName)
    {
        return upgradeCosts[buttonName];
    }

    public void SetNextUpgradeCost(string buttonName, int newCost)
    {
        upgradeCosts[buttonName] = newCost;
    }
}
