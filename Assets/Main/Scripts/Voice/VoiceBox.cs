using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceBox : MonoBehaviour
{
    public string message;

    private bool interacted;
    private Animation textbox;
    private TMP_Text text;


    void Start()
    {
        interacted = false;
        textbox = GameObject.Find("MAIN CANVAS").GetComponent<Animation>();
        text = GameObject.Find("GIRLTEXT").GetComponent<TMP_Text>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!interacted)
        {
            interacted = true;
            text.text = message;
            textbox.Play();
            Debug.Log("Message Played");
        }
    }
}
