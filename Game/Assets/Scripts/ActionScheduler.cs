using System;
using System.Collections;
using System.Collections.Generic;
using Island.Movement;
using UnityEngine;

//action scheduler taken from 312 course

namespace Island.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        //variable for IAction
        IAction currentAction;

        //function to start action sequence
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        //function to cancel action sequence
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}