using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NativeMover : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        GetComponent<NavMeshAgent>().destination = target.position;

    }


}