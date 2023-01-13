using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRespawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Respawn Triggered");
        col.GetComponent<Collider>().GetComponent<Rigidbody>().position = respawnPoint.position;
    }
}
