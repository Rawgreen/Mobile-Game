using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private float moveSpeed;

        private void Start()
        {
            moveSpeed = GetComponent<EnemyAttributes>().GetMoveSpeed();

            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }

        private void Update()
        {
            Vector3 moveDirection = CalculateDirection(target.transform);
            moveDirection.Normalize();

            // calculate rotation with atan2 and convert to degrees
            float rotationAngle = CalculateAngle(moveDirection);
            Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

            // pass new values to transform
            gameObject.transform.rotation = targetRotation;
            gameObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
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
