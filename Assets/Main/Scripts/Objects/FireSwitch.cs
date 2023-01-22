using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    [SerializeField] private Animator lever;
    [SerializeField] private Animator gate;
    [SerializeField] private Animator gate2;
    public GameObject txtToDisplay;             //display the UI text
    private bool PlayerInZone;                  //check if the player is in trigger
    private bool Switched;
   

    private void Start()
    {

        PlayerInZone = false;                   //player not in zone       
        txtToDisplay.SetActive(false);
        Switched = false;
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.E))//if in zone and press E key
        {
            lever.SetBool("SwitchFlipped", true);
            gate.SetBool("OpenGate", true);
            gate2.SetBool("OpenGate", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Switched == false)     //if player in zone
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
