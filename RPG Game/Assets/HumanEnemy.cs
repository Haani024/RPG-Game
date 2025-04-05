using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HumanEnemy : MonoBehaviour
{

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float movementSpeed = 3.5f;

    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private bool canAttack = true;
    private bool playerDetected = false;
    private bool isAttacking = false;
    public EnemyWeapon sword;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

       
        if (agent != null)
        {
            agent.speed = movementSpeed;
            agent.stoppingDistance = attackRadius * 0.9f;
            agent.isStopped = true;
        }
        else
        {
            Debug.LogError("NavMeshAgent component missing ");
        }

        
        if (animator == null)
        {
            Debug.LogError("Animator component missing");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool wasPlayerDetected = playerDetected;
        playerDetected = distanceToPlayer <= detectionRadius;

        
        if (playerDetected && !wasPlayerDetected)
        {
            Debug.Log("Player detected!");
        }
        else if (!playerDetected && wasPlayerDetected)
        {
           
            Debug.Log("Player lost!");
            agent.isStopped = true;
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }
            return;
        }

        if (playerDetected)
        {
            FacePlayer();

            if (distanceToPlayer > attackRadius)
            {
                
                if (!isAttacking && agent != null)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    
                   
                    if (agent.hasPath && agent.remainingDistance > agent.stoppingDistance)
                    {
                        if (animator != null)
                        {
                            animator.SetBool("IsMoving", true);
                        }
                    }
                }
            }
            else if (canAttack)
            {
               
                if (agent != null)
                {
                    agent.isStopped = true;
                }
                
                if (animator != null)
                {
                    animator.SetBool("IsMoving", false);
                }
                
                StartCoroutine(Attack());
            }
        }
        else
        {
            // No player detected
            if (agent != null)
            {
                agent.isStopped = true;
            }
            
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;
        
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        // Stop moving and play attack animation
        if (agent != null)
        {
            agent.isStopped = true;
        }
        
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("Attack");
        }

        // Wait for attack cooldown
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        isAttacking = false;
    }

    // Called by animation event
    public void ActivateSwordHitbox()
    {
        if (sword != null)
        {
            sword.StartAttackCollider();
        }
    }

    public void DeactivateSwordHitbox()
    {
        if (sword != null)
        {
            sword.EndAttackCollider();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
