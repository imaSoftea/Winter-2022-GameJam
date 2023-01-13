using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRockRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 350f, 0f) * Time.deltaTime);
    }
}
