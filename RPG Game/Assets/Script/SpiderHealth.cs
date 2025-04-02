using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SpiderHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    public int currentHealth;
    private Animator animator;
    public Image healthBarGreen;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
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
        
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        
        Destroy(gameObject, 5f);
        
        Debug.Log("Enemy Defeated! 50 XP awarded.");
    }
}