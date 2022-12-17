using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerCamHolder : MonoBehaviour
{
    public Transform cameraPos;

    void Update()
    {
        transform.position = cameraPos.position;
    }
}
