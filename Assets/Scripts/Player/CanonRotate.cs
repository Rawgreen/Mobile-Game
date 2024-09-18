using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotate : MonoBehaviour
{
    [SerializeField] private float radius = 4f;
    [SerializeField] private LayerMask enemyLayer;

    private GameObject closestEnemy;

    private void Update()
    {
        CalculateClosestEnemy();
    }

    public GameObject CalculateClosestEnemy()
    {
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, radius / 2.5f, enemyLayer);
        if (detectedEnemies.Length > 0)
        {
            float maxDistance = 10000f;
            foreach (Collider2D enemy in detectedEnemies)
            {
                //Phythagorean theorem
                float currentDistance = Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - gameObject.transform.position.x, 2) +
                                                   Mathf.Pow(enemy.transform.position.y - gameObject.transform.position.y, 2));
                if (currentDistance < maxDistance)
                {
                    maxDistance = currentDistance;
                    closestEnemy = enemy.gameObject;
                }
            }
            Debug.Log("closestEnemy is: " + closestEnemy.name);
            return closestEnemy;
        } 
        else
        {
            Debug.Log("No enemies detected");
            return null;
        }
    }
}
