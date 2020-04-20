using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //textures for the tree and then log when tree is cut 
    [SerializeField] GameObject logPile;
    [SerializeField] GameObject tree;
    GameObject logPileInstance;
    GameObject treeInstance;

    //item picked up 
    [SerializeField] Item woodItem;
    //players' inventory 
    InventorySystem inventorySystem;

    //tree will be cut down and then chopped 3 times
    private bool isCut = false;
    private int timesChopped = 0;
    private int maxTimesChopped = 3;

    private float timeLeft;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
    }

    private void Start()
    {
        //make the tree visible 
        treeInstance = Instantiate<GameObject>(tree, gameObject.transform);
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    public void Cut()
    {
        //create the log object 
        logPileInstance = Instantiate<GameObject>(logPile, gameObject.transform);
        //destroy tree
        Destroy(treeInstance);
        //change isCut to true
        isCut = true;
    }

    public void Interact() //called by player controller
    {
        if (!isCut) //not cut yet 
        {
            Cut();
        } else
        {
            Chop();
        }
    }

    private void Chop() //tree is cut down
    {
        if (timesChopped < maxTimesChopped-1)
        {
            //give player item
            inventorySystem.AddItem(woodItem, 1);
            timesChopped += 1;
        } else
        {
            //give item and destroy the game object
            inventorySystem.AddItem(woodItem, 1);
            Destroy(gameObject);
        }
    }
    
    //this is when the log has been chopped 3 times 
    private void OnDestroy()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.UpdateItems();
    }
}
