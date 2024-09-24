using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private CannonManager cannonManager;
    private CannonStats cannonStats;

    private Button damageButton;

    private void Start()
    {
        cannonStats = CannonManager.Instance.GetCannonStats();
        damageButton = GameObject.Find("DamageUpgrade").GetComponent<Button>();
    }

    private float GenerateRandomValue(float from, float to)
    {
        return Random.Range(from, to);
    }

    public void UpgradeDamage()
    {
        int amount = (int)GenerateRandomValue(1, 5);
        cannonStats.SetDamage(cannonStats.GetDamage() + amount);
        Debug.Log($"Damage upgraded by {amount}. New damage: {cannonStats.GetDamage()}");
    }

    public void UpgradeHealth()
    {
        int amount = (int)GenerateRandomValue(1, 5);
        cannonStats.SetTempHealth(cannonStats.GetTempHealth() + amount);
        cannonStats.SetMaxHealth(cannonStats.GetMaxHealth() + amount);
        Debug.Log($"Health upgraded by {amount}. New temp health: {cannonStats.GetTempHealth()}, New max health: {cannonStats.GetMaxHealth()}");
    }

    public void UpgradeProjectileSpeed()
    {
        float amount = GenerateRandomValue(0.1f, 1.0f);
        cannonStats.SetProjectileSpeed(cannonStats.GetProjectileSpeed() + amount);
        Debug.Log($"Projectile speed upgraded by {amount}. New projectile speed: {cannonStats.GetProjectileSpeed()}");
    }

    public void UpgradeRadius()
    {
        float amount = GenerateRandomValue(0.5f, 2.5f);
        cannonStats.SetRadius(cannonStats.GetRadius() + amount);
        Debug.Log($"Radius upgraded by {amount}. New radius: {cannonStats.GetRadius()}");
    }

    public void UpgradeShootingSpeed()
    {
        float amount = GenerateRandomValue(0.1f, 0.5f);
        cannonStats.SetShootingSpeed(cannonStats.GetShootingSpeed() + amount);
        Debug.Log($"Shooting speed upgraded by {amount}. New shooting speed: {cannonStats.GetShootingSpeed()}");
    }
}
