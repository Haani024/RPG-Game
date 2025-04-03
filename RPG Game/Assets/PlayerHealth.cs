using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    private Animator animator;

    public Image HealthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("You got hit, remaining health: " + currentHealth);
    
        
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }
        if (HealthBar!= null)
        {
            float newFillAmount = (float)currentHealth / maxHealth;
            
            HealthBar.fillAmount = newFillAmount;
           
        }
        
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        
        Destroy(gameObject);
        
        Debug.Log("You Died");
    }
}