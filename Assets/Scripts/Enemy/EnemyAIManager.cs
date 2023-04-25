using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIManager : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public FieldOfView fov;

    public Animator anim;

    //Patroling
    //[Header("Patrol Variables")]
    //public Vector3 walkPoint;
    public bool walkPointSet;
    //public float walkPointRange;

    public float range;

    public Vector3 lastKnownTargetPosition;

    [Header("Patrol Set Path")]
    public bool hasPatrolPattern;
    public List<Transform> waypoints;
    public int nextWaypoint;

    //Attacking
    [Header("Attacking Variables")]
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //States
    [Header("State Variables")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Player Is Touching");
            PlayerMovement.isHidden = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
