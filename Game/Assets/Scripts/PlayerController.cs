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

    private void Start()
    {
        LockCursor();
        Vector3 lastMousePos = Input.mousePosition;
    }

    private static void LockCursor()
    {
        //locks the cursor in the game so you can't see it 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        updateRotation();
    }

    private void updateRotation()
    {
        //calculate the y rotation of the head, this is used for the body also 
        float rotationY = head.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        //calculate the x rotation
        rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minimumY, maximumY);


        //only rotate the body in the Y direction 
        body.transform.localEulerAngles = new Vector3(0, rotationY, 0);
        //rotate the head in both directions
        head.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}
