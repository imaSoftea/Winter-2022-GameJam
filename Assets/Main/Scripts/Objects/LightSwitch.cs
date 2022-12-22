using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    Light light_switch;

    // Start is called before the first frame update
    void Start()
    {
        light_switch = GetComponent<Light>();
        light_switch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Lights Triggered");
        light_switch.enabled = true;
    }
}