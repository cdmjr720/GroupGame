using System;
using System.Collections;
using System.Collections.Generic;
using Island.Core;
using Island.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Island.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        //sets "target" for enemy to find player
        [SerializeField] Transform target;

        //calls mover variable from NativeMover Script
        NativeMover mover;

        //creates variable for player
        GameObject player;

        //creates vector variable for guard's position
        Vector3 guardPosition;

        //float variable for the last time enemy "saw" player
        float timeSinceLastSawPlayer = Mathf.Infinity;

        //establishes a variable for the waypoint index
        int currentWaypointIndex = 0;

        //calls NavMeshAgent
        NavMeshAgent navMeshAgent;

        bool range = false;

        private void Start()
        {
            mover = GetComponent<NativeMover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
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
                //calls suspicion behavior function
                //doesn't seem to be working right now
                SuspicionBehaviour();

            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        //establishes the patrol behavior for the enemy/native
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        //bool to register enemy's relation to waypoint
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            //function cycles through the waypoints
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        //Finds the current waypoint index for enemy/native to patrol
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }


        //function to call Cancel Action from ActionScheduler script for suspicious behavior
        //doesn't seem to work right now
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        //float created for DistanceToPlayer
        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void AttackBehaviour()
        {
            //for future attack behavior
        }

        // to find if enemy/native is in range of the player
        public bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
            
        }
        


        // colors radius around Native
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}