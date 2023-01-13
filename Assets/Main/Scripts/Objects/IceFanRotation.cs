using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFanRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(300f, 0f, 0f) * Time.deltaTime);
    }
}
