using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //This is for any object you can interact with.
    //It will include fish, trees, rocks, and other things

    bool isInteractable = true;

    public virtual void Interact()
    {

    }

    public void SetIsInteractableFalse()
    {
        isInteractable = false;
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

}
