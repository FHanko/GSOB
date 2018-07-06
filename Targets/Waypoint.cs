using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.Targets
{
    class Waypoint : MonoBehaviour
    {
        public Vector3 Goal;

        public Waypoint(Vector3 goal)
        {
            Goal = goal;
        }

        public float TickTime = 0f;
        public float MaxTickTime = 0f;
    }
}
