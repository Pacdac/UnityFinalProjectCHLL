using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject menu;
    [SerializeField] private NavMeshAgent babyAgent;
    [SerializeField] private DeviceBasedContinuousMoveProvider playerMoveProvider;
    [SerializeField] private VRBabyInteraction vrBabyInteraction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void SetPlayerSpeed(float speed)
    {
        playerMoveProvider.moveSpeed = speed;
        GameManager.PlayerSpeed = speed;
    }

    public void SetBabySpeed(float speed)
    {
        babyAgent.speed = speed;
        GameManager.BabySpeed = speed;
    }

    public void SetDangerTime(float dangerMaxTime)
    {
        vrBabyInteraction.maxTime = dangerMaxTime;
        GameManager.DangerMaxTime = dangerMaxTime;
    }
}
