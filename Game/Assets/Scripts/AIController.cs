using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //sets "chase distance" for enemy to find player
    [SerializeField] float chaseDistance = 3f;
    //sets "target" for enemy to find player
    [SerializeField] Transform target;

    private void Update()
    {
        //if player is within chase distance, sets enemy to move towards them
        if (DistanceToPlayer() < chaseDistance)
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }

    //float created for DistanceToPlayer
    private float DistanceToPlayer()
    {
        //finds player with "Player" tag
        GameObject player = GameObject.FindWithTag("Player");
        return Vector3.Distance(player.transform.position, transform.position);
    }
}
