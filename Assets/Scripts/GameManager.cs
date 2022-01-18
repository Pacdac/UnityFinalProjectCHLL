using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static bool isDangerInRange = false;
    private static List<Collider> dangersInRange;
    private static float babySpeed = 1.5f;
    private static float playerSpeed = 2f;
    private static float dangerMaxTime = 8f;
    public static GameObject carriedObject;
    public static GameObject carriableObject;
    public static GameObject interactableObject;
    public static float timeAlive = 0f;
    public static float timeInDanger = 0f;
    public static float timeInVision = 0f;

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        timeAlive = 0f;
        timeInDanger = 0f;
        timeInVision = 0f;
    }

    public static void Retry()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
        timeAlive = 0f;
        timeInDanger = 0f;
        timeInVision = 0f;
    }

    public bool IsDangerInRange
    {
        get => isDangerInRange;
        set => isDangerInRange = value;
    }

    public static List<Collider> DangersInRange
    {
        get => dangersInRange;
        set => dangersInRange = value;

    }

    public static float PlayerSpeed
    {
        get => playerSpeed;
        set => playerSpeed = value;

    }

    public static float BabySpeed
    {
        get => babySpeed;
        set => babySpeed = value;

    }

    public static float DangerMaxTime
    {
        get => dangerMaxTime;
        set => dangerMaxTime = value;

    }

    public static float ScoreCalculation()
    {
        return Mathf.Floor(timeAlive * 10 + timeInVision * 20 - timeInDanger * 5);  
    }

}