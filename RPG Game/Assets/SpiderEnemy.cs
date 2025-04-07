using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpiderEnemy : MonoBehaviour, IDamageable
{
    public float moveSpeed = 3f;
    public float attackDistance = 2f;
    public int attackDamage = 20;
    public float attackCooldown = 2f;
    public bool flipFacingDirection = true;
    public bool showAttackRange = true;
    private Transform player;
    private float nextAttackTime = 0f;
    private bool isAttacking = false;
    private Animator animator;
    [SerializeField] private int maxHealth = 30;
    public int currentHealth;
    public Image healthBarGreen;
    
    // NavMesh components
    private NavMeshAgent navMeshAgent;
    public float stoppingDistance = 1.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }
        
        animator = GetComponent<Animator>();
        
        // Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found! Please add a NavMeshAgent component to this GameObject.");
            return;
        }
        
        // Configure NavMeshAgent
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.updateRotation = !flipFacingDirection; // Let NavMesh handle rotation unless we're manually flipping
        
        FacePlayer();
        
        // Initialize health
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!player)
            return;
            
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackDistance && Time.time >= nextAttackTime && !isAttacking)
        {
            Debug.Log("Initiating attack!");
            Attack();
        }
        else if (!isAttacking)
        {
            MoveTowardPlayer();
        }
    }

    void MoveTowardPlayer()
    {
        // Set NavMeshAgent destination to player position
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        
        // Update animation
        if (animator)
            animator.SetBool("IsMoving", true);
            
        // Handle direction facing if needed
        if (flipFacingDirection)
            FacePlayer();
    }
    
    void FacePlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
        
        if (flipFacingDirection)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;
        isAttacking = true;
        
        // Stop movement during attack
        navMeshAgent.isStopped = true;
        
        FacePlayer();
        
        if (animator)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("Attack");
        }
        
        DealDamageToPlayer();
        
        Invoke("FinishAttack", 1f);
    }
    
    void DealDamageToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackDistance)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Spider boss attacked player for " + attackDamage + " damage!");
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on player!");
            }
        }
    }

    void FinishAttack()
    {
        isAttacking = false;
        // Resume movement after attack if player is still out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            navMeshAgent.isStopped = false;
        }
    }
    
    void OnDrawGizmos()
    {
        if (showAttackRange && enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage_002");
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
        // Stop movement
        navMeshAgent.isStopped = true;
        
        // Disable NavMeshAgent to prevent further movement
        navMeshAgent.enabled = false;
        
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        
        // Disable collision
        Collider enemyCollider = GetComponent<Collider>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }
        
        Destroy(gameObject, 5f);
        
        Debug.Log("Enemy Defeated! 50 XP awarded.");
    }
}