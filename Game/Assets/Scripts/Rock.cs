using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : InteractableObject
{
    //item picked up 
    [SerializeField] Item rockItem;
    //players' inventory 
    InventorySystem inventorySystem;

    //can be hit 2 times 
    private int timesHit = 0;
    private int maxTimesHit = 2;

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    public override void Interact()
    { 
        if (timesHit < maxTimesHit - 1)
        {
            inventorySystem.AddItem(rockItem, 1);
            timesHit++;
        } else
        {
            inventorySystem.AddItem(rockItem, 1);
            SetIsInteractableFalse();
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
