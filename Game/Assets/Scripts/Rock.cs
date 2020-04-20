using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
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

    public void Interact()
    {
        if (timesHit < maxTimesHit - 1)
        {
            inventorySystem.AddItem(rockItem, 1);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //This will update the player controllers list of interactable objects 
    }
}
