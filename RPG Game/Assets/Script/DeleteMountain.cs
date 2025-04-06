using UnityEngine;
using TMPro;

public class DeleteMountain : MonoBehaviour
{
    // Number of bolts required before mountains are deleted
    public int boltsRequired = 4;
    
    // Reference to the inventory UI container (should hold each item slot)
    public Transform inventoryContent;
    
    // Array of mountain GameObjects to be deleted
    public GameObject[] mountains;

    // Flag to ensure mountains are only deleted once
    private bool mountainsDeleted = false;

    void Update()
    {   
        if (mountainsDeleted) return;

        int boltCount = 0;
        Debug.Log("[DeleteMountain] Starting inventory check...");

        // Loop through each slot in the inventory
        foreach (Transform slot in inventoryContent)
        {
            Debug.Log("[DeleteMountain] Checking slot: " + slot.name);

            // Find the child that holds the item name (assumes the name is on a child called "ItemName")
            Transform nameObj = slot.Find("ItemName");

            if (nameObj != null)
            {
                var nameText = nameObj.GetComponent<TextMeshProUGUI>(); // For TextMeshPro

                if (nameText != null)
                {
                    Debug.Log("[DeleteMountain] Found item: " + nameText.text + " in slot: " + slot.name);

                    // Check if the item's name matches "BOLT"
                    if (nameText.text == "BOLT")
                    {
                        boltCount++;
                        Debug.Log("[DeleteMountain] BOLT found. Current bolt count: " + boltCount);
                    }
                }
                else
                {
                    Debug.Log("[DeleteMountain] TextMeshProUGUI component not found on " + nameObj.name);
                }
            }
            else
            {
                Debug.Log("[DeleteMountain] 'ItemName' child not found in slot: " + slot.name);
            }
        }

        Debug.Log("[DeleteMountain] Total BOLTs counted: " + boltCount);

        // If we've collected the required number of bolts, delete the mountains
        if (boltCount >= boltsRequired)
        {
            Debug.Log("[DeleteMountain] Required bolt count reached (" + boltCount + "). Deleting mountains.");

            foreach (GameObject mountain in mountains)
            {
                if (mountain != null)
                {
                    Debug.Log("[DeleteMountain] Deleting mountain: " + mountain.name);
                    Destroy(GameObject.Find("destroy"));
                    Destroy(GameObject.Find("destroy2"));

                }
                else
                {
                    Debug.Log("[DeleteMountain] A mountain reference is null.");
                }
            }
            
            mountainsDeleted = true;
        }
    }
}
