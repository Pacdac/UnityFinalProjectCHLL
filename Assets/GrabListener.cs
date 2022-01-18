using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            FindObjectOfType<AudioManager>().Play("Drop");
        }
            
    }
}
