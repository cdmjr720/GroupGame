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

    private void Start()
    {
        LockCursor();
        Vector3 lastMousePos = Input.mousePosition;
        characterController = GetComponent<CharacterController>();
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
    }

    private void movement()
    {
        if (characterController.isGrounded) 
        {
            //recalculate move direction 
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
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
}
