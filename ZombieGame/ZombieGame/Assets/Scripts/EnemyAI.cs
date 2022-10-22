using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private bool hasDied;
    private bool isAttacking = false;
    private int damageAmount = 20;

    public NavMeshAgent navMeshAgent;
    //patrolling 
    public Vector3[] waypoints;
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


        /*GameObject patrolPoints = Utils.Instance.PatrolPoints;
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(patrolPoints.transform.GetChild(i).transform);
            waypoints[i] = patrolPoints.transform.GetChild(i).transform;
        }*/
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex];
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

            // transform.LookAt(player);
            // move towards the player
            // navMeshAgent.SetDestination(player.position + new Vector3(1f, 0f, 1f));

            navMeshAgent.SetDestination(player.position + transform.forward * -2f);
        }
    }

    void CheckIsTooClose()
    {
        // DO THE ATTACK IN THIS FUNCTION
        
        //if distance between player and enemy is less than 2 it stops 
        if (dist <= 3)
        {

            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            animator.SetBool("isAttacking", true);
            //StartAttackMovement();

        }
        else
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
        }
        
        
        if (animator.GetBool("isAttacking") && !isAttacking)
        {
            playerController.Instance.TakeDamage(damageAmount);
            isAttacking = true;
        }

        if (!animator.GetBool("isAttacking"))
        {
            isAttacking = false;
        }

    }


    /*void StartAttackMovement()
    {
        if (animator.GetBool("isAttacking") && !isAttacking)
        {
            playerController.Instance.TakeDamage(damageAmount);
            isAttacking = true;
        }

        if (!animator.GetBool("isAttacking"))
        {
            isAttacking = false;
        }
    }*/
    
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
        /*if (Vector3.Distance(transform.position, target) < 0.005f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }*/
    }
    public void SetDamageAmount(int damage)
    {
        damageAmount = damage;
    }

    private void CheckHealth()
    {
        if (health <= 0 && hasDied == false)
        {
            // if it is dead play animation and stop enemy movement
            hasDied = true;
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            
            //animator.SetBool("isAttacking", false);
            //animator.SetBool("isMoving", false);
            
            animator.SetBool("isDead", true);
            

            //StopEnemyMovement();
            Destroy(gameObject, 10);

        }
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag("bullet") && hasDied == false)
        {
            gunBullet hitBullet = collision.gameObject.GetComponent<gunBullet>();
            
            // give this the gun damage
            TakeDamage(hitBullet.gunDamage);
            CheckHealth();
            Debug.Log(health);

        }

        /*if (collision.CompareTag("Player") && animator.GetBool("isAttacking"))
        {
            playerController.Instance.TakeDamage(damageAmount);
        }*/
        
    }
}
