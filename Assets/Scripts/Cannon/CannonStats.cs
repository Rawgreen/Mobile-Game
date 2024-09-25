using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Cannon Stat", menuName = "New Cannon Stat", order = 1)]
public class CannonStats : ScriptableObject
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int tempHealth;
    [SerializeField] private int damage = 10;
    [SerializeField] private int circleCorners = 720;
    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float shootingSpeed = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private LayerMask enemyLayer;

    private LineRenderer line;

    public void InitializeLineRenderer(LineRenderer lineRenderer)
    {
        line = lineRenderer;
        line.positionCount = circleCorners + 1;
        line.useWorldSpace = false;
        CreateCircle();
    }

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
        InitializeLineRenderer(line);
    }

    public float GetShootingSpeed()
    {
        return shootingSpeed;
    }

    public void SetShootingSpeed(float newShootingSpeed)
    {
        shootingSpeed -= newShootingSpeed;
        if (shootingSpeed <= 0.1f)
        {
            shootingSpeed = 0.1f;
        }
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

    public void SetProjectilePrefab(GameObject newPrefab)
    {
        projectilePrefab = newPrefab;
    }

    public LayerMask GetEnemyLayer()
    {
        return enemyLayer;
    }

    // In game
    void CreateCircle()
    {
        if (line == null)
        {
            Debug.LogError("LineRenderer is not initialized.");
            return;
        }

        Debug.Log("CreateCircle called"); // Debug log
        float angle = 0f;
        for (int i = 0; i < circleCorners + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / circleCorners;
        }
    }
}
