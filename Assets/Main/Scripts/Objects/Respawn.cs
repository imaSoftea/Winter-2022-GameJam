using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Respawn Triggered");

        AudioSource sound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
        sound.Play();

        col.GetComponent<Collider>().GetComponent<Rigidbody>().position = respawnPoint.position;
    }
}
