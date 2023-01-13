using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall2 : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall2", true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall2", false);
        }
    }
}
