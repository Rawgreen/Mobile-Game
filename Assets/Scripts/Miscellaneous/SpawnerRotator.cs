using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRotator : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float rotationSpeed = 1.0f;

    private float angle = 0f;

    private void Update()
    {
        angle += rotationSpeed * Time.deltaTime;

        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;

        gameObject.transform.position = new Vector3(x, y, transform.position.z);
        gameObject.transform.LookAt(centerPoint);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint.position, radius);
    }
}
