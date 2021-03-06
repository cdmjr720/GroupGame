﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=Ae7XJ8Ai42I

public class Fishing : InteractableObject
{
    //variable and bool for destination target (so fish "swims")
    public Transform[] target;
    bool isMoving = false;
    //creates new target to keep fish moving
    private Transform newTarget;
    //fish speed defined as 5
    float speed = 5.0f;

    //textures for the fish and for when fish is caught
    [SerializeField] GameObject fish;
    [SerializeField] Item fishItem;

    //allows for inventory system to be called
    InventorySystem inventorySystem;

    //relates to the difficulty of catching fish
    [SerializeField] [Range(1, 10)] float difficulty = 9f;

    private bool isCaught = false;

    private void Start()
    {
        //calls inventory system
        inventorySystem = FindObjectOfType<InventorySystem>();
    }



    public void Update()
    {

        //code for making fish "swim"
        if (isMoving == false)
        {
            //if fish is not moving, makes fish move to random target
            newTarget = target[Random.Range(0, target.Length)];
            
            isMoving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(transform.position, newTarget.position);

        if (transform.position == newTarget.position)
        {
            isMoving = false;
        }

    }

    public override void Interact() //called by player controller
    {
        Catch();
    }

    public void Catch()
    {
        inventorySystem.AddItem(fishItem, 1);
        SetIsInteractableFalse();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

}
