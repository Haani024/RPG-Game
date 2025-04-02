using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    // We start at 2 because child(0) & child(1) might be a panel + first text
    private int index = 2;

    void Update()
    {
        // Only proceed if dialogue is active
        if (MainPlayerMovementScript.dialouge)
        {
            // Check if the player pressed E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // If we still have more child objects to show
                if (index < transform.childCount)
                {
                    // Activate the next dialogue child
                    transform.GetChild(index).gameObject.SetActive(true);
                    index++;
                }
                else
                {
                    // No more children to show, reset index
                    index = 2;
                    MainPlayerMovementScript.dialouge = false;
                    
                    // Optionally hide the entire UI now that we're done
                    gameObject.SetActive(false);
                }
            }
        }
    }
}