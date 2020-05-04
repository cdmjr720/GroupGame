using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : InteractableObject
{
    [SerializeField] GameObject fireEmpty;
    [SerializeField] GameObject fireOn;
    GameObject fireEmptyInstance;
    GameObject fireOnInstance;
    GameObject player;
    bool fireLit = false;

    public void CreateFirePit() //called from crafting 
    {
        fireEmptyInstance = Instantiate<GameObject>(fireEmpty, gameObject.transform);
    }

    public override void Interact() //called by player controller
    {
        if (!fireLit) //not lit yet 
        {
            Light();
            SetIsInteractableFalse();
        }
    }

    //light fire 
    public void Light()
    {
        //get rid of unlit object and create lit object in same spot 
        Destroy(fireEmptyInstance);
        Instantiate<GameObject>(fireOn, gameObject.transform);
        fireLit = true;
    }

}
