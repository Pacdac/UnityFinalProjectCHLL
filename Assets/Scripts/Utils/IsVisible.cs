using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour
{

    Renderer m_Renderer;
    public Transform target;
       
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (m_Renderer.isVisible)
        {

            if (Physics.Raycast(transform.position, target.position-transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject.tag == "Player")
            {
                GameManager.timeInVision += Time.deltaTime;
                Debug.Log("Object is visible");
            } else
            {
                Debug.Log("Object is no longer visible");
            }
            
        }
        else Debug.Log("Object is no longer visible");
    }
    
}
