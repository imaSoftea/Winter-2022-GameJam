using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public S_PlayerCam playerCam;
    public GameObject adjust;
    public Vector3 momentum;

    public float angle;

    bool trigger;

    void OnTriggerEnter(Collider col)
    {
        col.GetComponent<Collider>().GetComponent<Rigidbody>().position = adjust.transform.position;
        col.GetComponent<Collider>().GetComponent<Rigidbody>().velocity = col.GetComponent<Collider>().GetComponent<Rigidbody>().velocity.magnitude * momentum;


        trigger = true;
    }
    void LateUpdate()
    {

        if (trigger)
        {
            playerCam.yRotation = angle;
            Debug.Log("Rotated");
        }
        trigger = false;

    }
}
