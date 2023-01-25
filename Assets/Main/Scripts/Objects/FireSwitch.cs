using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    [SerializeField] private Animator lever;
    [SerializeField] private Animator gate;
    [SerializeField] private Animator gate2;
    public GameObject switchText;               //display the UI text
    public GameObject keyText;
    private bool PlayerInZone;                  //check if the player is in trigger
    private bool Switched;
    public bool hasKey;
   

    private void Start()
    {

        PlayerInZone = false;                   //player not in zone       
        switchText.SetActive(false);
        Switched = false;
        hasKey = false;
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.E) && hasKey)//if in zone and press E key
        {
            lever.SetBool("SwitchFlipped", true);
            gate.SetBool("OpenGate", true);
            gate2.SetBool("OpenGate", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Switched == false && hasKey)     //if player in zone
        {
            switchText.SetActive(true);
            PlayerInZone = true;
        }
        else if (other.gameObject.tag == "Player" && Switched == false && !hasKey)
        {
            keyText.SetActive(true);
            PlayerInZone = true;
        }
    }


    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            switchText.SetActive(false);
            keyText.SetActive(false);
        }
    }
}
