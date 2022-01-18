using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveListener : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private bool isAlreadyWalking;
    private bool isCrounching;
    // Start is called before the first frame update


    // Update is called once per frame  
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        if (inputAxis != new Vector2(0, 0)) 
        {
            if (!isAlreadyWalking)
            {
                FindObjectOfType<AudioManager>().Play("FootSteps");
                isAlreadyWalking = true;
            }
            
        } else
        {
            FindObjectOfType<AudioManager>().Stop("FootSteps");
            isAlreadyWalking = false;
        }
    }
}
