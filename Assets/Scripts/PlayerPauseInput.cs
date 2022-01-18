using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class PlayerPauseInput : MonoBehaviour
{
    [SerializeField] private XRNode inputSourceLeft;
    [SerializeField] private XRNode inputSourceRight;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private DangerBar dangerBar;
    private bool inputMenuButton;
    private bool leftGrip;
    private bool rightGrip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        leftController.TryGetFeatureValue(CommonUsages.menuButton, out inputMenuButton);
        leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);
        rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);

        if (inputMenuButton)
        {
            if (!dangerBar.isActiveAndEnabled && (!leftGrip && !rightGrip) && Time.timeScale == 1)
            {
                pauseMenu.PauseGame();
            }

        }

    }

    
}
