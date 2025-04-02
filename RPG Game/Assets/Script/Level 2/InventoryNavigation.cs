using UnityEngine;
using UnityEngine.UI;

public class InventoryNavigation : MonoBehaviour
{
    [Header("Inventory References")]
    public GameObject inventoryPanel;
    public Button[] inventorySlots;
    public Button inventoryButton;
    public Button closeInventoryButton;

    [Header("Navigation Settings")]
    private int currentSelectedSlotIndex = 0;
    private bool isInventoryOpen = false;

    void Start()
    {
        // Initial setup of button listeners
        inventoryButton.onClick.AddListener(OpenInventory);
        closeInventoryButton.onClick.AddListener(CloseInventory);

        // Ensure inventory is closed on start
        inventoryPanel.SetActive(false);
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
}


