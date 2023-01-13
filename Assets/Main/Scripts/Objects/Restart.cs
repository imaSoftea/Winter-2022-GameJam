using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Restart");
            SceneManager.LoadScene("L_Ice");
        }
    }
}