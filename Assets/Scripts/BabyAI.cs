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
    AudioSource babyAudioSource;
    private Animator animator;
    bool isSettingNextPoint = false;

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
        
        if (movePoint != null) // car update appel� avant le start (?!), donc movePoint null 
        {
            agent.SetDestination(movePoint.position);
        } else {
            //movePoint = gameObject.transform; // pour dire de mettre un truc et �viter erreurs
            Debug.Log("hoooooooooooooooooooooooooooo");
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
                babyAudioSource.PlayDelayed(babyLaughLength); // PLAYDELAYED MARCHE PAS DONT ASK ME WHY
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
            //Debug.Log(movePoint.name + movePoint.position + " " + agent.destination + " " + agent.remainingDistance + " " + hasReachPoint);
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
            pointIndex++;
        }
        //Debug.Log(pointIndex + " " + pathPoints.Length);
        movePoint = pathPoints[pointIndex];
        Debug.Log("goto " + movePoint.name);
        MoveToPoint();
    }



    private void GetNextRoomPoints()
    {
        int nbRooms = fixedPoints.transform.childCount;
        bool noMoreRoom = roomIndex + 1 >= nbRooms;
        if (noMoreRoom)
        {
            Debug.Log("FIN DU PARCOURS");
            TurnOff();
            return;
        }

        roomIndex++;
        Debug.Log("Room " + roomIndex);

        Transform roomPathPoints = fixedPoints.transform.GetChild(roomIndex);
        pathPoints = roomPathPoints.GetComponentsInChildren<Transform>();
        pointIndex = 0; // start at index 1, cause index 0 = the parent itself
        for(int i = 0; i < pathPoints.Length; i++)
        {
            Debug.Log(pathPoints[i].name);
        }
        
       // Debug.Log("1er point " + pathPoints[pointIndex]);
    }

    public void TurnOff()
    {
        this.enabled = false;
    }
}