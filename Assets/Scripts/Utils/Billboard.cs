using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform billboardCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + billboardCamera.forward);
    }
}
