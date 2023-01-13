using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall3 : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall3", true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall3", false);
        }
    }
}
