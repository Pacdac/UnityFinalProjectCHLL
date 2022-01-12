using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BabyAI : MonoBehaviour
{
    // Suis les points de "paths", puis va chercher un danger random dans la
    [SerializeField] private GameObject fixedPoints; // room points
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator babyAnimator;
    private Transform[] pathPoints;
    private Transform movePoint;
    private int pointIndex;
    private bool isWalking = true;
    public float speed = 5f;
    public float rotationSpeed = 200f;
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
        
        SetMoveToDangerIfInRange();
        MoveToPoint();
        SetNextPointIfHasReached();
        Debug.Log(isWalking);
        babyAnimator.SetBool("isWalking", isWalking);
    }

    private void MoveToPoint()
    {
        
        if (movePoint) // car update appelé avant le start (?!), donc movePoint null 
        {
            agent.SetDestination(movePoint.position);
        } else {
            //movePoint = gameObject.transform; // pour dire de mettre un truc et éviter erreurs
        }
        
    }

    private void SetMoveToDangerIfInRange()
    {
        
        List<Collider> dangers = GameManager.DangersInRange;

        bool dangerInRange = dangers != null && dangers.Count > 0;
        if (dangerInRange)
        {

            movePointType = 1;
            isWalking = false;
            bool currentDangerStillInRange = IsDangerStillInRange(currentDanger);
            // select another danger
            if (currentDanger == null || !currentDangerStillInRange)
            {
                currentDanger = dangers[0];
                tempMovePoint = movePoint;
                movePoint = currentDanger.transform;
            }
            
        }
        else
        {
            isWalking = true;
            movePointType = 0;
            currentDanger = null;
            if (tempMovePoint != null)
            {
                movePoint = tempMovePoint; // go back to the defined path
                tempMovePoint = null;
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
        bool isMovePointADanger = movePointType == 1;
        if (!isMovePointADanger)
        {
            // distance between baby and object if danger, but distance = 0 if roomPoint
            // for a danger: agent.remainingDistance <= agent.stoppingDistance
            bool hasReachPoint = agent.remainingDistance - agent.stoppingDistance <= 0;

            if (hasReachPoint)
            {
                //Debug.Log("reached " + movePoint.name);
                SetNextPoint();
            }
        }
    }

    private void SetNextPoint()
    {
        pointIndex++;

        bool noMorePathPoints = pointIndex >= pathPoints.Length;
        if (noMorePathPoints)
        {
            GetNextRoomPoints();
        }
        //Debug.Log(pointIndex + " " + pathPoints.Length);
        movePoint = pathPoints[pointIndex];
        //Debug.Log("goto " + movePoint.name);
    }



    private void GetNextRoomPoints()
    {
        int nbRooms = fixedPoints.transform.childCount;
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            Debug.Log("FIN DU PARCOURS");
            GameManager.LoadNextLevel();
        }

        roomIndex++;
        Debug.Log("Room " + roomIndex);

        Transform roomPathPoints = fixedPoints.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 1; // start at index 1, cause index 0 = the parent itself
       // Debug.Log("1er point " + pathPoints[pointIndex]);
    }

    public void TurnOff()
    {
        this.enabled = false;
    }
}