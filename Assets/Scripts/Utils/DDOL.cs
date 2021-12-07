using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{

    public static GameObject carriedObject;
    public static GameObject carriableObject;
    public static GameObject interactableObject;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
