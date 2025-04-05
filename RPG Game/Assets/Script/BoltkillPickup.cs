using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoltkillPickup : MonoBehaviour
{
    [Header("Item Properties")]
    public string itemName = "Item";
    public Sprite itemSprite;

    [Header("Deadly Settings")]
    [Tooltip("Set to true if picking up this item should deal damage to the player.")]
    public bool isDeadly = false;
    [Tooltip("Amount of damage to deal when the item is deadly.")]
    public int damageAmount = 100;
    [Tooltip("If true, this item will only kill the player once.")]
    public bool onlyKillOnce = true;
    private bool hasKilledPlayer = false;

    [Header("Interaction Settings")]
    public float pickupRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public GameObject pickupPrompt;  // Optional UI element

    private bool playerInRange = false;
    private Transform player;

    void Start()
    {
        // Initialize pickup prompt
        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);

        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("Player not found! Make sure the player is tagged 'Player'.");
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            playerInRange = distance <= pickupRange;

            // Show or hide the pickup prompt based on distance
            if (pickupPrompt != null)
                pickupPrompt.SetActive(playerInRange);

            // Check for pickup input
            if (playerInRange && Input.GetKeyDown(interactKey))
            {
                TryPickup();
            }
        }
    }

    void TryPickup()
    {
        // Try to add the item to the inventory
        if (InventoryNavigation.instance.AddItem(itemName, itemSprite))
        {
            Debug.Log("Picked up: " + itemName);

            // If this item is flagged as deadly and hasn't yet killed the player (if onlyKillOnce is true), deal damage
            if (isDeadly && (!onlyKillOnce || !hasKilledPlayer))
            {
                if (HealthSystem.Instance != null)
                {
                    Debug.Log($"Item '{itemName}' is deadly. Dealing {damageAmount} damage to the player.");
                    HealthSystem.Instance.TakeDamage(damageAmount);
                    hasKilledPlayer = true;
                }
                else
                {
                    Debug.LogWarning("HealthSystem instance not found!");
                }
            }

            // Instead of destroying the object, disable further interaction:
            this.enabled = false; // Disable this script so it doesn't run again

            // Disable the collider so the player cannot interact with it further
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;

            // Hide the pickup prompt if it was active
            if (pickupPrompt != null)
                pickupPrompt.SetActive(false);
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }

    // Optional: Draw pickup range in the editor for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

