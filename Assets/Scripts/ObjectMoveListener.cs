using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMoveListener : MonoBehaviour
{

    private ConfigurableJoint joint;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
        if (transform.position != startPosition)
        {
            gameObject.tag = "Untagged";
        }
    }
}
