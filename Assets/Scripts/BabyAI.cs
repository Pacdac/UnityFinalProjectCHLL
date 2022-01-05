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
    private Transform tempMovePoint;

    private Collider currentDanger = null;

    void Start()
    {
        GetNextRoomPoints();
        movePoint = pathPoints[pointIndex];
    }

    void Update()
    {
        MoveToPoint();
        SetMoveToDangerIfInRange();

        SetNextPointIfHasReached();
    }

    private void MoveToPoint()
    {
        
        if (movePoint) // car update appelé avant le start (?!), donc movePoint null 
        {
            agent.SetDestination(movePoint.position);
        } else {
            movePoint = gameObject.transform; // pour dire de mettre un truc et éviter erreurs
        }
        
    }

    private void SetMoveToDangerIfInRange()
    {
        List<Collider> dangers = GameManager.DangersInRange;
        //Debug.Log(dangers.Count);

        if (dangers.Count > 0)
        {
            movePointType = 1;
            bool currentDangerStillInRange = IsDangerStillInRange(currentDanger);
            if (currentDanger == null || !currentDangerStillInRange)
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
            movePointType = 0;
            currentDanger = null;
            if (tempMovePoint)
            {
                movePoint = tempMovePoint; // go back to the defined path
            }
        }
    }

    private bool IsDangerStillInRange(Collider dangerToSearch)
    {
        if (dangerToSearch == null)
        {
            return false;
        }

        List<Collider> dangers = GameManager.DangersInRange;

        foreach (Collider danger in dangers)
        {
            if (danger.GetComponent<Transform>() == dangerToSearch.transform)
            {
                return true;
            }
        }
        return false;
    }

    private void SetNextPointIfHasReached()
    {
        bool hasReachPoint = agent.remainingDistance <= agent.stoppingDistance;
        if (hasReachPoint)
        {
            bool isMovePointADanger = movePointType == 1;

            if (isMovePointADanger)
            {
                return;
            }
            SetNextPoint();
        }
    }

    private void SetNextPoint()
    {
        pointIndex++;
        bool noMorePathPoints = pointIndex >= pathPoints.Length;
        if (noMorePathPoints)
        {
            //lookForDanger();
            GetNextRoomPoints();
            return;
        }
        movePoint = pathPoints[pointIndex];
    }

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
        Debug.Log(nbRooms);
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            Debug.Log("FIN DU PARCOURS");
            return;
        }

        roomIndex++;
        Debug.Log("Room " + roomIndex);

        Transform roomPathPoints = fixedPoints.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 1; // start at index 1, cause index 0 = the parent itself
    }
}