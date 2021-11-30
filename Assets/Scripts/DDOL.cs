using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{

    public static GameObject carriedObject;
    public static GameObject carriableObject;
    public static GameObject interactableObject;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
