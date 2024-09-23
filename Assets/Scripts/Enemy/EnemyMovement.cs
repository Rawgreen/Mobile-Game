using Miscellaneous;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private GameObject target;
        private SpawnSystem spawnSystem;
        private float moveSpeed;

        private void Awake()
        {
            spawnSystem = SpawnSystem.Instance;
            moveSpeed = spawnSystem.GetEnemyStats().GetMoveSpeed();
        }

        private void Start()
        {
            // Always target the "Cannon" GameObject
            target = GameObject.Find("Cannon");
            if (target == null)
            {
                Debug.LogError("Cannon GameObject not found in the scene.");
            }
        }

        private void Update()
        {
            if (target != null)
            {
                Vector3 moveDirection = CalculateDirection(target.transform);
                moveDirection.Normalize();

                // Calculate rotation with atan2 and convert to degrees
                float rotationAngle = CalculateAngle(moveDirection);
                Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

                // Pass new values to transform
                gameObject.transform.rotation = targetRotation;
                gameObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
            else
            {
                // Stop moving if the target is destroyed
                moveSpeed = 0;
            }
        }

        private Vector3 CalculateDirection(Transform target)
        {
            Vector3 direction = target.position - gameObject.transform.position;
            return direction.normalized;
        }

        private float CalculateAngle(Vector3 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        }
    }
}
