using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Collider swordCollider;
    public int damage = 10;
    private Transform player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        if (swordCollider == null)
        {
            swordCollider = GetComponent<Collider>();
            if (swordCollider == null)
            {
                Debug.LogError("No collider assigned or found on " + gameObject.name);
            }
        }
        
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }
    
    public void StartAttackCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
        }
    }

    public void EndAttackCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Hit player for " + damage + " damage!");
            }
        }
    }
}