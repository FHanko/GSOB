using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.Targets
{
    class Waypoint
    {
        public Vector3 Goal;

        public Waypoint(Vector3 goal)
        {
            Goal = goal;
        }
    }
}
