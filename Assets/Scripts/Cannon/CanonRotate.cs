using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotate : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    private float radius;
    private GameObject closestEnemy;

    private void Start()
    {
        radius = GetComponent<CannonAttributes>().GetRadius();
    }

    private void Update()
    {
        GameObject closestEnemy = CalculateClosestEnemy();
        RotateCannonTowardsEnemy(closestEnemy);
    }

    private void RotateCannonTowardsEnemy(GameObject closestEnemy)
    {
        if (closestEnemy != null)
        {
            Vector3 direction = CalculateDirection(closestEnemy.transform, gameObject.transform);
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotateTowardsEnemy = Quaternion.Euler(0, 0, rotationAngle);
            gameObject.transform.rotation = rotateTowardsEnemy;
        }
    }

    public GameObject CalculateClosestEnemy()
    {
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, radius / 2.5f, enemyLayer);
        if (detectedEnemies.Length > 0)
        {
            float maxDistance = 10000f;
            foreach (Collider2D enemy in detectedEnemies)
            {
                float currentDistance = CalculateDistance(enemy.transform, transform);
                if (currentDistance < maxDistance)
                {
                    maxDistance = currentDistance;
                    closestEnemy = enemy.gameObject;
                }
            }
            //TODO: Remove after testing
            Debug.Log("closestEnemy is: " + closestEnemy.name);
            return closestEnemy;
        } 
        else
        {
            //TODO: Remove after testing
            Debug.Log("No enemies detected");
            return null;
        }
    }

    //Phythagorean theorem
    private float CalculateDistance(Transform enemy, Transform currentObject)
    {
        return Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - currentObject.transform.position.x, 2) +
                          Mathf.Pow(enemy.transform.position.y - currentObject.transform.position.y, 2));
    }

    private Vector3 CalculateDirection(Transform target, Transform currentObject)
    {
        return (target.position - currentObject.position).normalized;
    }
}
