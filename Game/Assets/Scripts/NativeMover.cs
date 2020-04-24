using System.Collections;
using System.Collections.Generic;
using Island.Core;
using UnityEngine;
using UnityEngine.AI;

//script for making the native move, a lot of this was taken from 312 course

namespace Island.Movement
{
    public class NativeMover : MonoBehaviour, IAction
    {
        //variable to call NavMeshAgent
        NavMeshAgent navMeshAgent;
        
        private void Start()
        {
            //calls NMA
            navMeshAgent = GetComponent<NavMeshAgent>();
            
}

        void Update()
        {
            //will update animator when that is functioning
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            //I'm not sure why the following line DOESN'T work, or why it works when that line is commented out
            //            GetComponent<ActionScheduler>().StartAction(this);
            
            //moves to assigned destination
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}