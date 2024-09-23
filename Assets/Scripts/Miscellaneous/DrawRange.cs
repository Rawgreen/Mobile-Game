using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public class DrawRange : MonoBehaviour
    {
        private LineRenderer line;
        private CannonManager cannonManager;
        private CannonStats cannonStats;
        private float radius;
        private int circleCorners;

        private void Start()
        {
            cannonManager = CannonManager.Instance;
            cannonStats = cannonManager.GetCannonStats();
            radius = cannonStats.GetRadius();
            circleCorners = cannonStats.GetCircleCorners();

            if (line == null)
            {
                line = gameObject.GetComponent<LineRenderer>();
                line.positionCount = circleCorners + 1;
                line.useWorldSpace = false;
                CreateCircle();
            }
        }

        // In game
        void CreateCircle()
        {
            float angle = 0f;
            for (int i = 0; i < circleCorners + 1; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                line.SetPosition(i, new Vector3(x, y, 0));
                angle += 360f / circleCorners;
            }
        }

        //editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius / 2.5f);
        }
    }
}