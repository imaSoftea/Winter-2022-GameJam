using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHook : MonoBehaviour
{
    public GameObject Hook;
    public GameObject Hook2;
    public GameObject Display;
    public GameObject Aim;
    public GameObject txtToDisplay;             //display the UI text
    private bool PlayerInZone;                  //check if the player is in trigger
    private bool GotHook;

    public S_Grappling enableScript;


    private void Start()
    {
        PlayerInZone = false;                   //player not in zone       
        txtToDisplay.SetActive(false);
        GotHook = false;
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.E))//if in zone and press E key
        {
            Hook.SetActive(true);
            Hook2.SetActive(true);
            Display.SetActive(false);
            Aim.SetActive(true);
            GotHook = true;
            enableScript.hasGrapple = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GotHook == false)     //if player in zone
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
