using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{

    public static bool isCarriable = false;
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        /*Vector3 fwdRay = transform.TransformDirection(Vector3.forward) * 2.5f;
        Debug.DrawRay(transform.position, fwdRay, Color.green);*/

        isCarriable = false;
        if (Physics.Raycast(transform.position, fwd, out hit, 2.5f) && hit.collider.gameObject.tag == "Carriable")
        {
            Debug.Log("There is something carriable in front of the object!: " + hit.collider.gameObject.tag);
            isCarriable = true;
            PlayerMovement.carriableObject = hit.collider.gameObject;
        }
        
        
    }
}
