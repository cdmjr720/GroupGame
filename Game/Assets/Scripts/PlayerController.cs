using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player head and body 
    [SerializeField] GameObject body;
    [SerializeField] GameObject head;

    //looking sensitivity 
    [Range(1, 100)] [SerializeField] float sensitivityX = 10;
    [Range(1, 100)] [SerializeField] float sensitivityY = 10;

    //won't let you look straight up or straight down 
    float minimumY = -60F;
    float maximumY = 60F;

    //starts the player's x rotation at 0
    float rotationX = 0F;

    //character controller used for moving 
    CharacterController characterController;
    //other variables used for moving
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float gravity = 20f;
    Vector3 moveDirection = Vector3.zero;
    //holds vertical and horizontal values on keypress
    float horizontal;
    float vertical;
    float rotationDegree;
    float rotationRadian;
    float zMovementHorizontal;
    float xMovementHorizontal;
    float zMovementVertical;
    float xMovementVertical;
    float finalXMovement;
    float finalZMovement;

    //variables for cutting trees
    Tree[] allTrees;
    PlayerController player;
    float distanceToClosestTree = Mathf.Infinity;
    Tree closestTree;

    //variables for catching fish
    Fishing[] allFish;
    float distanceToClosestFish = Mathf.Infinity;
    Fishing closestFish;


    private void Start()
    {
        LockCursor();
        Vector3 lastMousePos = Input.mousePosition;
        characterController = GetComponent<CharacterController>();
        player = FindObjectOfType<PlayerController>();
        allTrees = FindObjectsOfType<Tree>();
        allFish = FindObjectsOfType<Fishing>();
    }

    private static void LockCursor()
    {
        //locks the cursor in the game so you can't see it 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        movement();
        updateRotation();
        interact();
        fishing();
    }

    private void interact()
    {
        foreach (Tree tree in allTrees)
        {
            if (Vector3.Distance(tree.transform.position, player.transform.position) < distanceToClosestTree)
            {
                distanceToClosestTree = Vector3.Distance(tree.transform.position, player.transform.position);
                closestTree = tree;
            }
        }

        if (distanceToClosestTree < 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                closestTree.Interact();
            }
        }

        foreach (Fishing fish in allFish)
        {
            if (Vector3.Distance(fish.transform.position, player.transform.position) < distanceToClosestFish)
            {
                distanceToClosestFish = Vector3.Distance(fish.transform.position, player.transform.position);
                closestFish = fish;
            }
        }

        if (distanceToClosestFish < 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                closestFish.Interact();
            }
        }

    }

    private void fishing()
    {


    }

    private void movement()
    {
        if (characterController.isGrounded) 
        {
            //recalculate move direction 
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            //get the rotation of the player
            rotationDegree = transform.eulerAngles.y;
            rotationRadian = ToRadians(rotationDegree);
            if (vertical != 0)
            {
                //the z element of the movement is the starting "forward and backward" 
                //this is the cos of the angle when going forward and the negative sin when going backward 
                //vertical will usually be a value near 1 or -1 and gets closer to those values while the button is held 
                zMovementVertical = (float)(Math.Cos((double)rotationRadian)) * vertical;
                //the x element of the movement is the starting "left and right"
                //this is the sin of the angle when going forward and the negative cos when going backward 
                //horizontal will usually be a value near 1 or -1 and gets closer to those values while the button is held 
                xMovementVertical = (float)(Math.Sin((double)rotationRadian)) * vertical;
            }
            if (horizontal != 0) //right movement 
            {
                //add .5PI to the rotation in radians before calculations. This is equivalent to rotating by 90 degrees
                //and then using the same calculations as moving forward and backward. 
                //see vertical for explanation on the z and x elements 
                zMovementHorizontal = (float)(Math.Cos((double)rotationRadian + .5 * Math.PI)) * horizontal;
                xMovementHorizontal = (float)(Math.Sin((double)rotationRadian + .5 * Math.PI)) * horizontal;
            }
            //average the movement values 
            finalXMovement = (xMovementHorizontal + xMovementVertical) / 2;
            finalZMovement = (zMovementHorizontal + zMovementVertical) / 2;

            //stop the player from drifting 
            if (finalXMovement < .2 && finalXMovement > -.2 && finalZMovement < .2 && finalZMovement > -.2)
            {
                finalXMovement = 0;
                finalZMovement = 0;
            }

            //create the new movement vector with the values calculated;
            moveDirection = new Vector3(finalXMovement, 0f, finalZMovement);
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //apply gravity the first time 
        //gravity is applied twice because it is an acceleration 
        moveDirection.y -= gravity * Time.deltaTime;

        //move the controller 
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void updateRotation()
    {
        //calculate the y rotation of the head, this is used for the body also 
        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        //calculate the x rotation
        rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);


        //rotate the body in the y direction, this also rotates the head in the y direction 
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        //rotate the head in the x direction
        head.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);
    }


    private float ToRadians(float angle)
    {
        float newAngle = (float)(angle * Math.PI / 180);
        return newAngle;
    }

    public void UpdateItems()
    {
        allTrees = FindObjectsOfType<Tree>();
        allFish = FindObjectsOfType<Fishing>();
    }
}
