using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CanonRotate : MonoBehaviour
    {
        private CannonManager cannonManager;

        private void Start()
        {
            cannonManager = GetComponent<CannonManager>();
        }

        private void Update()
        {
            GameObject closestEnemy = cannonManager.GetClosestEnemy();
            if (closestEnemy != null)
            {
                gameObject.transform.rotation = CalculateDirection(closestEnemy.transform);
            }
        }

        private Quaternion CalculateDirection(Transform target)
        {
            Vector3 direction = (target.position - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}
