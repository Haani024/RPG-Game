using System.Threading;
using UnityEngine;

public class SpiderHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        Thread.Sleep(10000);
        animator.Play("Death");
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    
        
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage_002");
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

        
        Destroy(gameObject, 2f);
        XPManager.Instance.AddXP(50);
        Debug.Log("Enemy Defeated! 50 XP awarded.");
    }
}
