using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance { get; private set; } // Singleton instance

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Elements")]
    public Slider healthSlider;
    public Text healthText;

    private CharacterController controller;
    private Animator animator;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Set initial health
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Ensure CharacterController is reattached after scene reload
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Prevent errors if the CharacterController is missing
        if (controller == null) return;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public void UpgradeHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth; // Fully heal when upgrading health

        UpdateHealthUI();
    }

    private void Die()
    {
        Debug.Log("Player has died! Restarting level...");
        
        // Prevent errors by disabling movement before reloading
        enabled = false;

        // Reset health before scene reload
        currentHealth = maxHealth;

        // Reload scene safely
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign CharacterController after scene reload
        controller = GetComponent<CharacterController>();

        // Reset UI references
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();

        // Reset health UI
        UpdateHealthUI();

        // Re-enable script
        enabled = true;
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + " / " + maxHealth;
        }
    }
}
