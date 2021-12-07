using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyAI : MonoBehaviour
{
    // Suis les points de "paths", puis va chercher un danger random dans la
    [SerializeField] private GameObject paths;
    [SerializeField] private GameObject danger;
    private Transform[] pathPoints;
    private Transform[] dangerPoints;
    private Transform movePoint;
    private int pointIndex;
    private float speed = 5f;
    private float rotationSpeed = 200f;
    private int roomIndex = -1; // le mettre dans GameManager maybe

    void Start()
    {
        getNextRoomPoints();
    }

    void Update()
    {
        move();
<<<<<<< Updated upstream:Assets/BabyAI.cs
        rotate();

        bool hasReachPoint = Vector3.Distance(transform.position, movePoint.position) == 0;
        if (hasReachPoint)
        {
            unactiveDanger(); // pourrait mettre une variable pour désactiver que si danger, mais osef de désactiver les autres points
            setNextPoint();
=======

        bool hasReachPoint = agent.remainingDistance == 0;
        Debug.Log(hasReachPoint + "-----" + movePoint.position + "-" + agent.remainingDistance);
        if (hasReachPoint)
        {
            waitForDangerToBeRemoved();
>>>>>>> Stashed changes:Assets/Scripts/BabyAI.cs
        }
    }

    private void move()
    {
        agent.SetDestination(movePoint.position);
    }

    private void waitForDangerToBeRemoved()
    {
<<<<<<< Updated upstream:Assets/BabyAI.cs
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
    }

    private void rotate()
    {
        Vector3 position = movePoint.position - transform.position;
        float angle = Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, angle - 90, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angle - 90, 0), rotationSpeed * Time.deltaTime);
    }
=======
        bool isFacingDanger = GameManager.GetInstance().IsFacingDanger;
        if (!isFacingDanger)
        {
            movePoint.gameObject.SetActive(false);
            setNextPoint();
        }
    }



>>>>>>> Stashed changes:Assets/Scripts/BabyAI.cs
    private void setNextPoint()
    {
        pointIndex++;
        bool noMorePathPoints = pointIndex >= pathPoints.Length;
        if (noMorePathPoints)
        {
            lookForDanger();
            return;
        }
        movePoint = pathPoints[pointIndex];
    }

    private void lookForDanger()
    {
        Transform roomDangerPoints = danger.transform.GetChild(roomIndex);
        dangerPoints = roomDangerPoints.GetComponentsInChildren<Transform>();
        int nbDanger = dangerPoints.Length - 1;

        bool noMoreDanger = nbDanger <= 0;
        if (noMoreDanger)
        {
            getNextRoomPoints();
            return;
        }

        int randomDangerIndex = Random.Range(1, nbDanger);
        movePoint = dangerPoints[randomDangerIndex];
    }

    private void getNextRoomPoints()
    {
        int nbRooms = paths.transform.childCount;
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            Debug.Log("End of the path");
            return;
        }

        roomIndex++;
        Debug.Log("Room " + roomIndex);

        Transform roomPathPoints = paths.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 1; // start at index 1, cause index 0 = the parent itself
        movePoint = pathPoints[pointIndex];
    }
}
