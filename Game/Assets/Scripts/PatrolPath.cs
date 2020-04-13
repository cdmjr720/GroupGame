using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//patrol path code (taken from 312 course)

namespace Island.Control
{
    public class PatrolPath : MonoBehaviour
    {
        //float for radius for gizmo
        const float waypointGizmoRadius = 0.3f;

        //function to draw gizmos on patrol path/children
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        //finds and returns the children of the patrol path (waypoints)
        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}