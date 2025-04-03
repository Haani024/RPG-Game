using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickupItem : MonoBehaviour
{
    [Header("Item Properties")]
    public string itemName = "Item";
    public Sprite itemSprite;
    
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        // Check if player is in range
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            playerInRange = distance <= pickupRange;
            
            // Show/hide pickup prompt
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
        // Try to add to inventory
        if (InventoryNavigation.instance.AddItem(itemName, itemSprite))
        {
            // Success! Item was added
            Debug.Log("Picked up: " + itemName);
            
            // Destroy the object
            Destroy(gameObject);
            
            pickupPrompt.SetActive(false);
            
            
        }
        else
        {
            // Inventory full
            Debug.Log("Inventory full!");
            // You could display a message to the player here
        }
        
        
    }

   
    
    
    // Optional: Draw pickup range in editor for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
    
    
}