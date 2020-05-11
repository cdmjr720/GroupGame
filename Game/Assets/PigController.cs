using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : InteractableObject
{
    //variable and bool for destination target
    [SerializeField] Transform[] target;
    bool isMoving = false;
    //creates new target to keep pig moving
    private Transform newTarget;
    //pig speed defined as 5
    float speed = 5.0f;

    //allows for inventory system to be called
    InventorySystem inventorySystem;

    [SerializeField] Item porkItem;

    private bool isCaught = false;

    private void Start()
    {
        //calls inventory system
        inventorySystem = FindObjectOfType<InventorySystem>();
    }



    public void Update()
    {
        Move();

    }

    private void Move()
    {
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
        inventorySystem.AddItem(porkItem, 1);
        SetIsInteractableFalse();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
