using UnityEngine;
using UnityEngine.AI;

public class CerberusChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 20f;
    public float attackRange = 2f;
    public float damage = 20f;
    public float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackPlayer()
    {
        // Make sure the player has a health script to damage
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Add bite animation, sound, or particle here
        Debug.Log("Cerberus bites the player!");
    }
}