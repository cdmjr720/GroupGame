using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    [SerializeField] float chaseDistance = 3f;
    [SerializeField] Transform target;

    private void Update()
    {
        if (DistanceToPlayer() < chaseDistance)
        {
            //Debug.Log("Should Chase");
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }

    private float DistanceToPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return Vector3.Distance(player.transform.position, transform.position);
    }
}
