using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "New Enemy Stats", order = 2)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int tempHealth;
    [SerializeField] private float moveSpeed = 0.33f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int pointsWorth = 10;
    [SerializeField] private int goldsWorth = 1;

    public void SetTempHealth(int health)
    {
        tempHealth = health;
    }

    public int GetTempHealth()
    {
        return tempHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public int GetPointsWorth()
    {
        return pointsWorth;
    }

    public int GetGoldsWorth()
    {
        return goldsWorth;
    }
}
