using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public static class HelperFunctions
    {
        public static float CalculateDistance(Transform origin, Transform target)
        {
            return Mathf.Sqrt(Mathf.Pow(target.position.x - origin.position.x, 2) +
                              Mathf.Pow(target.position.y - origin.position.y, 2));
        }
    }
}
