using Cannon;
using Miscellaneous;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUpgrade : MonoBehaviour
{
    private CannonManager cannonManager;
    private CannonStats cannonStats;

    private void Start()
    {
        cannonStats = CannonManager.Instance.GetCannonStats();
    }

    public void UpgradeDamage(int amount)
    {
        cannonStats.SetDamage(cannonStats.GetDamage() + amount);
    }

    public void UpgradeHealth(int amount)
    {
        cannonStats.SetTempHealth(cannonStats.GetTempHealth() + amount);
        cannonStats.SetMaxHealth(cannonStats.GetMaxHealth() + amount);
    }

    public void UpgradeProjectileSpeed(float amount)
    {
        cannonStats.SetProjectileSpeed(cannonStats.GetProjectileSpeed() + amount);
    }

    public void UpgradeRadius(float amount)
    {
        cannonStats.SetRadius(cannonStats.GetRadius() + amount);
    }

    public void UpgradeShootingSpeed(float amount)
    {
        cannonStats.SetShootingSpeed(cannonStats.GetShootingSpeed() + amount);
    }
}
