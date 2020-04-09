using System;
using System.Collections;
using System.Collections.Generic;
using Island.Movement;
using UnityEngine;

namespace Island.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
        public void CancelCurrentAction()
        {
            StartAction(null);
        }

        public interface IAction
        {
            void Cancel();
        }

    }



}