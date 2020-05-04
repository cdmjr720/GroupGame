using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player head and body and hand
    [SerializeField] GameObject body;
    [SerializeField] GameObject head;
    [SerializeField] Transform handLocation;
    //looking sensitivity 
    [Range(1, 100)] [SerializeField] float sensitivityX = 10;
    [Range(1, 100)] [SerializeField] float sensitivityY = 10;

    //won't let you look straight up or straight down 
    float minimumY = -60F;
    float maximumY = 60F;

    //starts the player's x rotation at 0
    float rotationX = 0F;

    //crafting GUI
    [SerializeField] GameObject craftingView;

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

    //reach distance for interacting with things
    [SerializeField] float reachDistance = 6f;
    //text on canvas to tell player to press E
    [SerializeField] Text interactAlertText;

    //interaction variables 
    InteractableObject[] allInteractables;
    float nearestObjectDistance = Mathf.Infinity;
    InteractableObject nearestObject;

    //player object
    PlayerController player;


    private void Start()
    {
        craftingView.SetActive(false);
        interactAlertText.enabled = false;
        LockCursor();
        Vector3 lastMousePos = Input.mousePosition;
        characterController = GetComponent<CharacterController>();
        player = FindObjectOfType<PlayerController>();
        allInteractables = FindObjectsOfType<InteractableObject>();
    }

    private static void LockCursor()
    {
        //locks the cursor in the game so you can't see it 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        UpdateInteractables();
        movement();
        updateRotation();
        InteractWithObject();
        Crafting();
    }

    private void Crafting()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftingView.activeInHierarchy)
            {
                craftingView.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            } else
            {
                craftingView.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    //check for new items
    private void UpdateInteractables()
    {
        allInteractables = FindObjectsOfType<InteractableObject>();
    }

    //interact with the closest object if player is closer than 3 units 
    private void InteractWithObject()
    {
        //find the closest object 
        foreach (InteractableObject interactableObject in allInteractables)
        {
            if (Vector3.Distance(interactableObject.transform.position, player.transform.position) < nearestObjectDistance && interactableObject.IsInteractable())
            {
                nearestObjectDistance = Vector3.Distance(interactableObject.transform.position, player.transform.position);
                nearestObject = interactableObject;
            }
        }

        //within range
        if (nearestObjectDistance < reachDistance)
        {
            //show the interact alert 
            interactAlertText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                nearestObject.Interact();
                //reset variables so that if the object is destroyed it will no longer be in the array 
                nearestObject = null;
                nearestObjectDistance = Mathf.Infinity;
            }
        } else
        {
            interactAlertText.enabled = false;
        }
        nearestObjectDistance = Mathf.Infinity;
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

    public Transform GetHandLocation()
    {
        return handLocation;
    }
    
}
