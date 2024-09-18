using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject target = null;

    private Vector3 moveDirection;
    private float rotationAngle;


    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        moveDirection = target.transform.position - gameObject.transform.position;
        moveDirection.Normalize();

        // calculate rotation with atan2 and convert to degrees
        rotationAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

        gameObject.transform.rotation = targetRotation;
        gameObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
