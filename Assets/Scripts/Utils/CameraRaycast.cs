using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{

    public static bool isCarriable = false;
    public static bool isInteractable = false;
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        /*Vector3 fwdRay = transform.TransformDirection(Vector3.forward) * 2.5f;
        Debug.DrawRay(transform.position, fwdRay, Color.green);*/

        isCarriable = false;
        isInteractable = false;

        if (Physics.Raycast(transform.position, fwd, out hit, 2.5f))
            //Debug.Log("Tag: " + hit.collider.gameObject.tag);      

        if (Physics.Raycast(transform.position, fwd, out hit, 2.5f) && hit.collider.gameObject.tag == "Carriable")
        {
            isCarriable = true;
            DDOL.carriableObject = hit.collider.gameObject;
        }

        if (Physics.Raycast(transform.position, fwd, out hit, 2.5f) && hit.collider.gameObject.tag == "Interactable")
        {
            isInteractable = true;
            DDOL.interactableObject = hit.collider.gameObject;
            
        }

        


    }
}
