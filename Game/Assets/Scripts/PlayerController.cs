using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 100)] [SerializeField] float sensitivityX = 10;
    [Range(1, 100)] [SerializeField] float sensitivityY = 10;

    float minimumY = -60F;
    float maximumY = 60F;

    float rotationY = 0F;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 lastMousePos = Input.mousePosition;
    }
    // Update is called once per frame
    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

    }
}
