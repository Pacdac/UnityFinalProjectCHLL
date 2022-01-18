using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BabyAI : MonoBehaviour
{

    [SerializeField] private GameObject fixedPoints; // room points
    [SerializeField] private NavMeshAgent agent;
    private Transform[] pathPoints;
    private Transform movePoint;
    private int pointIndex;
    private int roomIndex = -1;
    int movePointType;
    private Transform tempMovePoint;
    private Collider currentDanger = null;
    AudioSource babyAudioSource;
    private Animator animator;

    void Start()
    {
        animator = gameObject.transform.Find("BabyModel").GetComponent<Animator>();
        babyAudioSource = GameObject.Find("/Baby/Baby").GetComponent<AudioSource>();
        babyAudioSource.Play();

        GetNextRoomPoints();
        movePoint = pathPoints[++pointIndex];
    }

    void Update()
    {
        SetMoveToDangerIfInRange();
        MoveToPoint();
        SetNextPointIfHasReached();
    }

    private void MoveToPoint()
    {
        if (movePoint != null)
        {
            agent.SetDestination(movePoint.position);
        }
    }

    private void SetMoveToDangerIfInRange()
    {
        List<Collider> dangers = GameManager.DangersInRange;

        bool dangerInRange = dangers != null && dangers.Count > 0;
        if (dangerInRange)
        {

            movePointType = 1;
            bool currentDangerStillInRange = IsDangerStillInRange(currentDanger);
            animator.SetBool("isMoving", false);
            // select another danger
            if (currentDanger == null || !currentDangerStillInRange)
            {
                babyAudioSource.Pause();
                FindObjectOfType<AudioManager>().Play("Laugh");
                float babyLaughLength = FindObjectOfType<AudioManager>().SearchSound("Laugh").clip.length;
                babyAudioSource.PlayDelayed(babyLaughLength);
                animator.SetBool("isMoving", true);

                currentDanger = dangers[0];
                tempMovePoint = movePoint;
                movePoint = currentDanger.transform;
            }    
        }
        else
        {
            movePointType = 0;
            currentDanger = null;
            if (tempMovePoint != null)
            {
                animator.SetBool("isMoving", true);
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
            bool hasReachPoint = agent.remainingDistance - agent.stoppingDistance <= 0;
            if (hasReachPoint)
            {
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
            pointIndex++;
        }
        //Debug.Log(pointIndex + " " + pathPoints.Length);
        movePoint = pathPoints[pointIndex];
        MoveToPoint();
    }


    private void GetNextRoomPoints()
    {
        int nbRooms = fixedPoints.transform.childCount;
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            Debug.Log("End of the path");
            TurnOff();
            return;
        }

        roomIndex++;

        Transform roomPathPoints = fixedPoints.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 0; // start at index 1, cause index 0 = the parent itself
    }

    public void TurnOff()
    {
        this.enabled = false;
    }
}