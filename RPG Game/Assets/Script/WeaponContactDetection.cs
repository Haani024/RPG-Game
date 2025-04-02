using UnityEngine;

public class WeaponContactDetection : MonoBehaviour
{
    public Collider swordCollider; 
    public int damage = 10; // Adjust damage as needed
    
    
    void Start()
    {
        swordCollider.enabled = false; 
    }
    public void StartAttackCollider()
    {
        swordCollider.enabled = true; 
    }

    public void EndAttackCollider()
    {
        swordCollider.enabled = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}

