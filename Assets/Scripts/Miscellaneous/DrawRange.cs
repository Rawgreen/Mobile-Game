using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public class DrawRange : MonoBehaviour
    {
        private CannonStats cannonStats;

        private void Start()
        {
            cannonStats = FindObjectOfType<CannonManager>().GetCannonStats();
            LineRenderer line = gameObject.GetComponent<LineRenderer>();

            // Initialize the LineRenderer in CannonStats
            cannonStats.InitializeLineRenderer(line);
        }
    }
}