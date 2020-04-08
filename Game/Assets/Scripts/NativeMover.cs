using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//original mover for native, will probably delete

public class NativeMover : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        GetComponent<NavMeshAgent>().destination = target.position;

    }


}