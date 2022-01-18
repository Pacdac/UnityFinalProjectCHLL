using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour
{

    Renderer m_Renderer;
    public Transform target;
    public Vector3 offset;
    private Vector3 targetPositionWithOffset;
       
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        targetPositionWithOffset = target.position + offset;
        if (m_Renderer.isVisible)
        {
            Debug.Log("Object is rendered");
            if (Physics.Raycast(transform.position, targetPositionWithOffset - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject.tag == "Player")
            {
                GameManager.timeInVision += Time.deltaTime;
                Debug.Log("Object is visible");
            } else
            {
                Debug.Log("Object is no longer visible");
            }
            
        }
        else
        {
            Debug.Log("Object is not rendered");
        }
    }

}
