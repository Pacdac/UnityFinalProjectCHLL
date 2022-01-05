using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BabyAI : MonoBehaviour
{
    // Suis les points de "paths", puis va chercher un danger random dans la
    [SerializeField] private GameObject fixedPoints; // room points
    [SerializeField] private GameObject danger;
    [SerializeField] private NavMeshAgent agent;
    private Transform[] pathPoints;
    private Transform[] dangerPoints;
    private Transform movePoint;
    private int pointIndex;
    private float speed = 5f;
    private float rotationSpeed = 200f;
    private int roomIndex = -1; // le mettre dans GameManager maybe
    int movePointType;
    private Transform tempMovePoint = null;

    private Collider currentDanger = null;

    void Start()
    {
        GetNextRoomPoints();

    }

    void Update()
    {
        MoveToPoint();
        GoTowardsDangerIfInRange();

        // checkIfHasReachedPoint();
    }

    private void MoveToPoint()
    {
        agent.SetDestination(movePoint.position);
    }

    private void GoTowardsDangerIfInRange()
    {
        List<Collider> dangers = GameManager.DangersInRange;
        Debug.Log(dangers.Count);
        if (dangers.Count > 0)
        {
            if (currentDanger == null)
            {
                currentDanger = dangers[0];
                tempMovePoint = movePoint;
                movePoint = currentDanger.transform;
            }
            else
            {
                // rien, on continue
            }

        }
        else
        {
            currentDanger = null;
            movePoint = tempMovePoint; // go back to the defined path
        }
    }

    /*private void checkIfHasReachedPoint()
    {
        bool hasReachPoint = agent.remainingDistance <= agent.stoppingDistance;
        if (hasReachPoint)
        {
            bool isMovePointADanger = movePointType == 1;
<<<<<<< Updated upstream
            bool isDangerInRange = GameManager.GetInstance().IsDangerInRange;
=======
            bool isDangerInRange = GameManager.IsDangerInRange;
            Debug.Log(GameManager.IsDangerInRange);
>>>>>>> Stashed changes
            if (isMovePointADanger && isDangerInRange)
            {
                return;
            }
            setNextPoint();
        }
    }*/

    /*private void setNextPoint()
    {
        pointIndex++;
        bool noMorePathPoints = pointIndex >= pathPoints.Length;
        if (noMorePathPoints)
        {
            lookForDanger();
            return;
        }
        movePoint = pathPoints[pointIndex];
    }*/

    /*private void lookForDanger()
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
<<<<<<< Updated upstream

        GameManager.GetInstance().CurrentDanger = movePoint;
    }
=======
        GameManager.CurrentDanger = movePoint;
    }*/


    private void GetNextRoomPoints()
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
        Debug.Log(movePoint);
    }

}