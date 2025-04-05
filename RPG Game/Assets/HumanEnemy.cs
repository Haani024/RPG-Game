using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class HumanEnemy : MonoBehaviour, IDamageable
{
    public Image healthBarGreen;
    [SerializeField] private float detectionRadius = 60f;
    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private int maxHealth = 40;
    public int currentHealth;

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
        currentHealth = maxHealth;

       
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

        if (direction != Vector3.zero && !isAttacking)
        {
            animator.SetBool("IsMoving", false);
            //transform.rotation = Quaternion.LookRotation(direction);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                5f * Time.deltaTime);

        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        int attacknum = Random.Range(0, 3);

        // Stop moving and play attack animation
        if (agent != null)
        {
            agent.isStopped = true;
        }
        
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            if (attacknum == 0)
                animator.SetTrigger("Attack");
            else if (attacknum == 1)
            {
                animator.SetTrigger("Attack2");
            }
            else
            {
                animator.SetTrigger("Attack3");
            }
            
        }

        
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        isAttacking = false;
    }

    // Called by animations
    public void ActivateSwordHitbox()
    {
        if (sword != null)
        {
            sword.StartAttackCollider();
        }
        agent.updatePosition = false;
        agent.updateRotation = false;
        animator.applyRootMotion = true;
    }

    public void DeactivateSwordHitbox()
    {
        if (sword != null)
        {
            sword.EndAttackCollider();
        }
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.Warp(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    
        
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }
        if (healthBarGreen != null)
        {
            float newFillAmount = (float)currentHealth / maxHealth;
            
            healthBarGreen.fillAmount = newFillAmount;
           
        }
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        
        Destroy(gameObject, 5f);
        
        Debug.Log("Enemy Defeated! 50 XP awarded.");
    }
}
