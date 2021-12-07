using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{

    public static GameObject carriedObject;
    public static GameObject carriableObject;
    public static GameObject interactableObject;
    public static float timeAlive = 0f;
    public static float timeInDanger = 0f;

    private void Update()
    {
        CalculateTimeAlive();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void CalculateTimeAlive()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timeAlive += Time.deltaTime;
            Debug.Log("Time spent alive: " + timeAlive);
        }
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
