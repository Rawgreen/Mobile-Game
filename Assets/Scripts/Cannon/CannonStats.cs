using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "New Stats", order = 1)]
public class CannonStats : ScriptableObject
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int tempHealth;
    [SerializeField] private int damage = 10;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float shootingSpeed = 1f;
    [SerializeField] private int circleCorners = 360;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private LayerMask enemyLayer;

    public int GetTempHealth()
    {
        return tempHealth;
    }

    public void SetTempHealth(int health)
    {
        tempHealth = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public float GetRadius()
    {
        return radius;
    }

    public void SetRadius(float newRadius)
    {
        radius = newRadius;
    }

    public float GetShootingSpeed()
    {
        return shootingSpeed;
    }

    public void SetShootingSpeed(float newShootingSpeed)
    {
        shootingSpeed = newShootingSpeed;
    }

    public int GetCircleCorners()
    {
        return circleCorners;
    }

    public void SetCircleCorners(int newCircleCorners)
    {
        circleCorners = newCircleCorners;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public void SetProjectileSpeed(float newProjectileSpeed)
    {
        projectileSpeed = newProjectileSpeed;
    }

    public GameObject GetProjectilePrefab()
    {
        return projectilePrefab;
    }

    public LayerMask GetEnemyLayer()
    {
        return enemyLayer;
    }
}
