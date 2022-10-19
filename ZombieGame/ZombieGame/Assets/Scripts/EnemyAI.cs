using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private bool hasDied;
  

    public NavMeshAgent navMeshAgent;
    //patrolling 
    public Transform[] waypoints;
    int waypointIndex = 0;
    Vector3 target;

    //chasing
    private Transform player;
    private float dist;
    public float moveSpeed;
    // how close we are to player until enemy starts chasing us
    public float howClose;


    //animation related stuff
   private Animator animator;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        hasDied = false;
       
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();

        player = GameObject.FindGameObjectWithTag("Player").transform;


        //initialize animation
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", true);
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        navMeshAgent.SetDestination(target);
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    public void TakeDamage(int hitAmount)
    {
        health = health - hitAmount;
    }
    void ChasePlayer()
    {
     
        // distance between player and enemy
        dist = Vector3.Distance(player.position, transform.position);

        if(dist <= howClose && hasDied == false)
        {
            CheckIsTooClose();

            //transform.LookAt(player);
            // move towards the player
            navMeshAgent.SetDestination(player.position);
        }
    }

    void CheckIsTooClose()
    {
        // DO THE ATTACK IN THIS FUNCTION
        Debug.Log(dist);
        //if distance between player and enemy is less than 2 it stops 
        if (dist <= 2)
        {

            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            animator.SetBool("isAttacking", true);
        }
        else
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
        }

    }


    void StopEnemyMovement()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
        // run animation stops
        animator.SetBool("isMoving", false);
    }

    void StartEnemyMovement()
    {
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.isStopped = false;
        // run animation starts
        animator.SetBool("isMoving", true);

    }

    

    // Update is called once per frame
    void Update()
    {
        

        ChasePlayer();
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        if (health <= 0 && hasDied == false)
        {
            // if it is dead play animation and stop enemy movement
            hasDied = true;
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            animator.SetBool("isDead", true);


            //StopEnemyMovement();
            Destroy(gameObject, 10);

        }

        if (collision.tag== "bullet" && hasDied == false)
        {
            Debug.Log(health);
            // give this the gun damage
            TakeDamage(20);
            
        }
        
    }
}
