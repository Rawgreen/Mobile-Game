using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private CannonStats cannonStats;

    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    private Dictionary<string, TextMeshProUGUI> buttonTexts = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, float> upgradeAmounts = new Dictionary<string, float>();

    private void Start()
    {
        cannonStats = CannonManager.Instance.GetCannonStats();

        InitializeButton("DamageUpgrade", 1f, 5f);
        InitializeButton("HealthUpgrade", 1f, 5f);
        InitializeButton("RadiusUpgrade", 0.5f, 2.5f);
        InitializeButton("ProjectileVelocityUpgrade", 0.1f, 1.0f);
        InitializeButton("ShootingRateUpgrade", 0.1f, 0.5f);
    }

    private void InitializeButton(string buttonName, float minValue, float maxValue)
    {
        Button button = GameObject.Find(buttonName).GetComponent<Button>();
        buttons[buttonName] = button;

        TextMeshProUGUI buttonText = GetTextComponent(button);
        buttonTexts[buttonName] = buttonText;

        float upgradeAmount = GenerateRandomValue(minValue, maxValue);
        upgradeAmounts[buttonName] = upgradeAmount;

        UpdateValueText(buttonName, buttonText, upgradeAmount);
    }

    private float GenerateRandomValue(float from, float to)
    {
        return Random.Range(from, to);
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
        UpgradeStat("ProjectileSpeedUpgrade", () => cannonStats.SetProjectileSpeed(cannonStats.GetProjectileSpeed() + upgradeAmounts["ProjectileSpeedUpgrade"]));
    }

    public void UpgradeRadius()
    {
        UpgradeStat("RadiusUpgrade", () => cannonStats.SetRadius(cannonStats.GetRadius() + upgradeAmounts["RadiusUpgrade"]));
    }

    public void UpgradeShootingSpeed()
    {
        // => is a lambda expression. It is a way to pass a function as a parameter.
        // () means no parameters used in the lambda expression.
        UpgradeStat("ShootingSpeedUpgrade", () => cannonStats.SetShootingSpeed(cannonStats.GetShootingSpeed() + upgradeAmounts["ShootingSpeedUpgrade"]));
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
            case "ProjectileSpeedUpgrade": return cannonStats.GetProjectileSpeed();
            case "RadiusUpgrade": return cannonStats.GetRadius();
            case "ShootingSpeedUpgrade": return cannonStats.GetShootingSpeed();
            default: return 0;
        }
    }

    private float GetMinValue(string buttonName)
    {
        switch (buttonName)
        {
            case "DamageUpgrade":
            case "HealthUpgrade": return 1f;
            case "RadiusUpgrade": return 0.5f;
            case "ProjectileSpeedUpgrade": return 0.1f;
            case "ShootingSpeedUpgrade": return 0.1f;
            default: return 0;
        }
    }

    private float GetMaxValue(string buttonName)
    {
        switch (buttonName)
        {
            case "DamageUpgrade":
            case "HealthUpgrade": return 5f;
            case "RadiusUpgrade": return 2.5f;
            case "ProjectileSpeedUpgrade": return 1.0f;
            case "ShootingSpeedUpgrade": return 0.5f;
            default: return 0;
        }
    }

    private void UpdateValueText(string buttonName, TextMeshProUGUI buttonText, float value)
    {
        if (buttonName == "DamageUpgrade" || buttonName == "HealthUpgrade")
        {
            buttonText.text = $"+{(int)value}";
        }
        else
        {
            buttonText.text = $"+{value:F1}";
        }
    }

    private TextMeshProUGUI GetTextComponent(Button button)
    {
        TextMeshProUGUI[] textComponents = button.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var textComponent in textComponents)
        {
            if (textComponent.name == "Value")
            {
                return textComponent;
            }
        }

        return null;
    }
}
