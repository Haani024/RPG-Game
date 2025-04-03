using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEnemy : MonoBehaviour
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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }
        
        animator = GetComponent<Animator>();
        
        
        FacePlayer();
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
       //ima change this later to better movement
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        
        FacePlayer();
        
        
        if (animator)
            animator.SetBool("IsMoving", true);
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
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
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
    }
    
    void OnDrawGizmos()
    {
        if (showAttackRange && enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}

