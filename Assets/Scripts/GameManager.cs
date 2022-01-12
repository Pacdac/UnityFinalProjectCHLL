using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    /*private static GameManager _instance;
    private bool isDangerInRange = false;
    private Transform currentDanger;
    private GameManager()*/

    private static bool isFacingDanger = false;
    private static bool isDangerInRange = false;
    //private static Transform currentDanger;
    private static List<Collider> dangersInRange;
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

    public static float ScoreCalculation()
    {
        return Mathf.Floor(timeAlive * 10 + timeInVision * 20 - timeInDanger * 5);  
    }

}