using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidFire : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene("L_Fire");
    }
}
