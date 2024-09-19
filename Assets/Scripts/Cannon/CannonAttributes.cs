using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttributes : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int damage = 10;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float shootingSpeed = 1f;
    [SerializeField] private int circleCorners = 360;

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
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
}
