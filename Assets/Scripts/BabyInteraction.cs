using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyInteraction : MonoBehaviour
{

    public float lookRadius = 3f;
    public float maxTime = 5f;
    public DangerBar dangerBar;
    private bool isDead = false;
    private float currentTime = 0f;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Update()
    {
        List<Collider> dangerous = new List<Collider>();
        RaycastHit hit;
        Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, lookRadius);
        GameManager.timeAlive += Time.deltaTime;
        checkDeath();

        //Transform currentDanger = GameManager.CurrentDanger;
        bool isDangerInRange = false;

        foreach (Collider collider in collidersInRadius)
        {
            if ((collider.tag == "Interactable" && collider.GetComponent<InteractableAsset>().isOpen) || collider.tag == "Carriable" && !collider.GetComponent<Rigidbody>().isKinematic)
            {

                /*if (collider.GetComponent<Transform>() == currentDanger)
                {
                    isDangerInRange = true;
                }*/
                if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject.tag == "Carriable")
                {
                    dangerous.Add(collider);
                    /*if (collider.GetComponent<Transform>() == currentDanger)
                    {
                        isDangerInRange = true;
                    }*/
                }
                //dangerous.Add(collider);

            }
        }

        //GameManager.GetInstance().IsDangerInRange = isDangerInRange;
        GameManager.DangersInRange = dangerous;
        
        if (dangerous.Count > 0)
        {
            //FindObjectOfType<AudioManager>().Play("Laugh");
            startTimer();
        }
        else if (dangerous.Count == 0 && !isDead)
        {
            resetTimer();
        }
    }

    private void startTimer()
    {
        //FindObjectOfType<AudioManager>().Stop("Baby");
        
        //FindObjectOfType<AudioManager>().Play("Baby");
        Debug.Log("test");

        dangerBar.gameObject.SetActive(true);
        if (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
            GameManager.timeInDanger += Time.deltaTime;
            dangerBar.SetTime(currentTime);
        }

    }

    private void resetTimer()
    {
        currentTime = 0f;
        dangerBar.SetInitialTime(maxTime);
        dangerBar.gameObject.SetActive(false);
    }

    private void checkDeath()
    {
        if (currentTime > maxTime)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            FindObjectOfType<AudioManager>().Play("Dead");
            GameManager.LoadNextLevel();
        }
    }
}