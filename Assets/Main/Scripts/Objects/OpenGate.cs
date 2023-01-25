using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public GameObject txtToDisplay;             //display the UI text
    private bool PlayerInZone;                  //check if the player is in trigger
    bool GateOpen;

    private void Start()
    {
        PlayerInZone = false;                   //player not in zone       
        txtToDisplay.SetActive(false);
        GateOpen = false;
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.E))//if in zone and press E key
        {
            anim.SetBool("GateOpen", true);
            GateOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GateOpen == false)     //if player in zone
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }


    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
}
