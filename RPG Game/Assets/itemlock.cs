using UnityEngine;
using TMPro; // Use this if you're using TextMeshPro

public class Itemlock : MonoBehaviour
{
    public Animator animator;
    public int itemsRequired = 3;
    public Transform inventoryContent;

    private bool doorOpened = false;

    void Update()
    {
        if (doorOpened) return;

        int bottleCount = 0;

        foreach (Transform slot in inventoryContent)
        {
            Transform nameObj = slot.Find("ItemName");

            if (nameObj != null)
            {
                var nameText = nameObj.GetComponent<TextMeshProUGUI>(); // For TMP
                // If you're using regular UI.Text, replace the line above with:
                // var nameText = nameObj.GetComponent<Text>();

                if (nameText != null && nameText.text == "Bottle")
                {
                    bottleCount++;
                }
            }
        }

        if (bottleCount >= itemsRequired)
        {
            Debug.Log("Bottle count reached. Forcing door open.");
            animator.Play("big door open2"); // <-- This skips the trigger & just plays it
            doorOpened = true;
        }
    }
}
