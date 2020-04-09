using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//script for Native "Patrol"

public class NativeMover : MonoBehaviour
{
    const float waypointGizmoRadius = 0.3f;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
        }
    }

    private void Update()
    {

    }


}