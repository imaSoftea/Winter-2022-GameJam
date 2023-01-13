using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall", true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("PlayFall", false);
        }
    }
}
