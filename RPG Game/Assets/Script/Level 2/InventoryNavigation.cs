using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro namespace

// Extend your existing InventoryNavigation script
public class InventoryNavigation : MonoBehaviour
{
    [Header("Inventory References")]
    public GameObject inventoryPanel;
    public Button[] inventorySlots;
    public Button inventoryButton;
    public Button closeInventoryButton;

    [Header("Inventory Items")]
    public TMP_Text[] itemNameTexts;    // Changed to TMP_Text for TextMeshPro
    public Image[] itemImages;      // Add array of Image components to display item sprites

    [Header("Navigation Settings")]
    private int currentSelectedSlotIndex = 0;
    private bool isInventoryOpen = false;
    
    // Make this a singleton for easy access
    public static InventoryNavigation instance;

    void Awake()
    {
        // Setup singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Initial setup of button listeners
        inventoryButton.onClick.AddListener(OpenInventory);
        closeInventoryButton.onClick.AddListener(CloseInventory);

        // Ensure inventory is closed on start
        inventoryPanel.SetActive(false);
        
        // Initialize item arrays if needed
        if (itemNameTexts == null || itemNameTexts.Length == 0)
        {
            itemNameTexts = new TMP_Text[inventorySlots.Length];
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // Look for TextMeshPro component instead
                itemNameTexts[i] = inventorySlots[i].GetComponentInChildren<TMP_Text>();
            }
        }
        
        if (itemImages == null || itemImages.Length == 0)
        {
            itemImages = new Image[inventorySlots.Length];
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // Finding item image - modify this based on your actual hierarchy
                // This assumes there's an Image component on a child of the button
                Transform imageTransform = inventorySlots[i].transform.Find("ItemImage");
                if (imageTransform != null)
                    itemImages[i] = imageTransform.GetComponent<Image>();
                else
                    itemImages[i] = inventorySlots[i].transform.GetChild(0).GetComponent<Image>();
                
                // Make sure the image is initially transparent/empty
                if (itemImages[i] != null)
                {
                    Color transparent = new Color(1, 1, 1, 0);
                    itemImages[i].color = transparent;
                }
            }
        }
        Debug.Log("Inventory Slots Debug:");
        for (int i = 0; i < itemNameTexts.Length; i++) 
        {
            if (itemNameTexts[i] != null)
                Debug.Log("Slot " + i + " text: '" + itemNameTexts[i].text + "'");
            else
                Debug.Log("Slot " + i + " text component is null");
        }
        
         for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (itemNameTexts[i] != null)
                    itemNameTexts[i].text = "";
                    
                if (itemImages[i] != null)
                {
                    itemImages[i].sprite = null;
                    itemImages[i].color = new Color(1, 1, 1, 0); // Make transparent
                }
            }
    }

    void Update()
    {
        // Toggle inventory with 'I' key
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }

        // Close inventory with 'C' key
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseInventory();
        }

        // Navigation within inventory when open
        if (isInventoryOpen)
        {
            HandleInventoryNavigation();
        }
    }

    void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        isInventoryOpen = true;
        
        // Select first slot when opening
        if (inventorySlots.Length > 0)
        {
            currentSelectedSlotIndex = 0;
            HighlightCurrentSlot();
        }
    }

    void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
        
        // Remove any existing highlights
        if (inventorySlots.Length > 0)
        {
            ResetSlotHighlights();
        }
    }

    void HandleInventoryNavigation()
    {
        // Navigate right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelection(1);
        }
        // Navigate left
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelection(-1);
        }
        // Navigate down (assuming a 2x3 grid)
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(2);
        }
        // Navigate up
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(-2);
        }
    }

    void MoveSelection(int offset)
    {
        // Reset previous slot highlight
        ResetSlotHighlights();

        // Calculate new index
        currentSelectedSlotIndex = Mathf.Clamp(
            currentSelectedSlotIndex + offset, 
            0, 
            inventorySlots.Length - 1
        );

        // Highlight new slot
        HighlightCurrentSlot();
    }

    void HighlightCurrentSlot()
    {
        if (inventorySlots[currentSelectedSlotIndex] != null)
        {
            // Change color or add visual indication of selection
            inventorySlots[currentSelectedSlotIndex].GetComponent<Image>().color = Color.yellow;
        }
    }

    void ResetSlotHighlights()
    {
        foreach (Button slot in inventorySlots)
        {
            if (slot != null)
            {
                slot.GetComponent<Image>().color = Color.white;
            }
        }
    }

    // New method for adding items to inventory
    public bool AddItem(string itemName, Sprite itemSprite)
    {
        // Find first empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (itemNameTexts[i] != null && string.IsNullOrEmpty(itemNameTexts[i].text))
            {
                // This slot is empty, add the item here
                Debug.Log($"[AddItem] Slot {i} - assigning item '{itemName}'");
                itemNameTexts[i].text = itemName;
                
                if (itemImages[i] != null)
                {
                    itemImages[i].sprite = itemSprite;
                    itemImages[i].color = Color.white; // Make visible
                }
                
                return true; // Successfully added
            }
        }
        
        // If we get here, inventory is full
        Debug.Log("Inventory is full!");
        return false;
    }
    
    // New method to remove items (optional, useful for using/dropping items)
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
        {
            if (itemNameTexts[slotIndex] != null)
                itemNameTexts[slotIndex].text = "";
            
            if (itemImages[slotIndex] != null)
            {
                itemImages[slotIndex].sprite = null;
                itemImages[slotIndex].color = new Color(1, 1, 1, 0); // Make transparent
            }
        }
    }
}
   

