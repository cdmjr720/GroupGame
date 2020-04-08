using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AIController : MonoBehaviour
{
    //sets "chase distance" for enemy to find player
    [SerializeField] float chaseDistance = 3f;
    //sets "target" for enemy to find player
    [SerializeField] Transform target;

    GameObject player;
    IAction currentAction;



    //for patrol functions:
    [SerializeField] float suspicionTime = 3f;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    private void Start()
    {
        //finds player with "Player" tag
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        //if player is within chase distance, sets enemy to move towards them
        if (DistanceToPlayer() < chaseDistance)
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }

        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            
        }

    }

    //float created for DistanceToPlayer
    private float DistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void SuspicionBehaviour(IAction action)
    {
        currentAction.Cancel();
    }


    // Called by Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    //calls IAction interface
    public interface IAction
    {
        void Cancel();
    }

}

