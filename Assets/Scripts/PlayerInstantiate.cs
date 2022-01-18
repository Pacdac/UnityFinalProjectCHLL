using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DeviceBasedContinuousMoveProvider>().moveSpeed = GameManager.PlayerSpeed;        
    }

}
