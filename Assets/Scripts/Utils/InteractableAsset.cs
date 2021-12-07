using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAsset : MonoBehaviour
{
    public bool isOpen = false;

    public void onInteraction()
    {
        isOpen = !isOpen;
        GetComponent<Animator>().SetBool("open", isOpen);
    }
}
