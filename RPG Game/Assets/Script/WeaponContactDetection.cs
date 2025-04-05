using UnityEngine;

public class WeaponContactDetection : MonoBehaviour
{
    public Collider swordCollider; 
    public int damage = 10; 
    
    
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
        IDamageable enemyHealth = other.GetComponent<IDamageable>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}

