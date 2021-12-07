using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BabyAI : MonoBehaviour
{
    // Suis les points de "paths", puis va chercher un danger random dans la
    [SerializeField] private GameObject fixedPoints;
    [SerializeField] private GameObject danger;
    [SerializeField] private NavMeshAgent agent;
    private Transform[] pathPoints;
    private Transform[] dangerPoints;
    private Transform movePoint;
    private int pointIndex;
    private float speed = 5f;
    private float rotationSpeed = 200f;
    private int roomIndex = -1; // le mettre dans GameManager maybe
    private int movePointType; // 0 : path, 1 : danger

    void Start()
    {
        getNextRoomPoints();
        move();
    }

    void Update()
    {
        bool hasReachPoint = agent.remainingDistance <= agent.stoppingDistance;
        if (hasReachPoint)
        {
            if (movePointType == 1)
            {
                waitForDangerToBeRemoved();
                return;
            }
            setNextPoint();
        }

        move();
    }


    private void waitForDangerToBeRemoved()
    {
        bool isFacingDanger = GameManager.GetInstance().IsFacingDanger;
        GameManager.GetInstance().CurrentDanger = movePoint;
        
        if (!isFacingDanger)
        {
            //movePoint.gameObject.SetActive(false);
            Debug.Log(isFacingDanger);
            setNextPoint();
        }
    }

    private void move()
    {
        agent.SetDestination(movePoint.position);
    }

    private void setNextPoint()
    {
        pointIndex++;
        bool noMorePathPoints = pointIndex >= pathPoints.Length;
        Debug.Log(noMorePathPoints);
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
        movePointType = 1;
        agent.stoppingDistance = 2;
    }

    private void getNextRoomPoints()
    {
        int nbRooms = fixedPoints.transform.childCount;
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            //Debug.Log("End of the path");
            return;
        }

        roomIndex++;
        Debug.Log("Room " + roomIndex);

        Transform roomPathPoints = fixedPoints.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 1; // start at index 1, cause index 0 = the parent itself
        movePoint = pathPoints[pointIndex];

        movePointType = 0;
        agent.stoppingDistance = 0;
    }
    
}
