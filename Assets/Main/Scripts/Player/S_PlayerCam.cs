using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerCam : MonoBehaviour
{
    [Header("Sensitivity")]
    //Sensitivity
    public float horzSens;
    public float vertSens;

    //Orientation
    public Transform orientation;

    //Rotation
    float xRotation;
    public float yRotation;

    void Start()
    {
        //Locks Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Input
        float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * horzSens * 500;
        float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * vertSens * 500;

        yRotation += xMouse;
        xRotation -= yMouse;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //clamps from looking below the ground

        //Rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); //moves camera
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); //moves player
    }
}
