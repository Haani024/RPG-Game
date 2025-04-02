// // using UnityEngine;
// // using TMPro;
// //
// // public class NPCsystem : MonoBehaviour
// // {
// //     [Header("Dialogue Settings")]
// //     // Dialogue prefab (assign your dialogue prefab in the Inspector)
// //     public GameObject d_template; 
// //     // Reference to the Canvas where dialogue elements will be added (assign in Inspector)
// //     public Transform canva;
// //     
// //     bool player_detection = false;
// //
// //     void Update()
// //     {
// //
// //         if (player_detection && Input.GetKeyDown(KeyCode.E) && !MainPlayerMovementScript.dialouge)
// //         {
// //             Debug.Log("Conditions met: player_detection = " + player_detection 
// //                       + ", E pressed, dialouge = " + MainPlayerMovementScript.dialouge);
// //
// //             // Activate the canvas by calling SetActive on its GameObject
// //             canva.gameObject.SetActive(true);
// //             Debug.Log("Canvas set to active.");
// //
// //             MainPlayerMovementScript.dialouge = true;
// //             Debug.Log("Dialogue set to true.");
// //             
// //             // Instantiate dialogue entries
// //             Debug.Log("Instantiating dialogue entries...");
// //             NewDialogue("hey");
// //             NewDialogue("What's good");
// //             NewDialogue("Karan has done no work :p");
// //
// //             // Check if the canvas has at least two children before activating the second child
// //             if (canva.childCount > 1)
// //             {
// //                 canva.GetChild(1).gameObject.SetActive(true);
// //                 Debug.Log("Activating child at index 1 on the canvas.");
// //             }
// //             else
// //             {
// //                 Debug.LogWarning("Canvas does not have a child at index 1 to activate.");
// //             }
// //
// //             Debug.Log("Player detection");
// //         }
// //     }
// //
// //     void NewDialogue(string text)
// //     {
// //         Debug.Log("NewDialogue called with text: " + text);
// //
// //         // Instantiate the dialogue prefab as a child of the assigned canvas
// //         GameObject template_clone = Instantiate(d_template, canva);
// //         Debug.Log("Instantiated dialogue prefab under canvas.");
// //
// //         // Ensure the instantiated prefab has a second child to set the text on
// //         if (template_clone.transform.childCount > 1)
// //         {
// //             TextMeshProUGUI tmp = template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
// //             if (tmp != null)
// //             {
// //                 tmp.text = text;
// //                 Debug.Log("Assigned text to prefab child: " + text);
// //             }
// //             else
// //             {
// //                 Debug.LogWarning("The second child of the dialogue prefab does not have a TextMeshProUGUI component.");
// //             }
// //         }
// //         else
// //         {
// //             Debug.LogWarning("The dialogue prefab does not have a second child for the text component.");
// //         }
// //     }
// //
// //     private void OnTriggerEnter(Collider other)
// //     {
// //         Debug.Log("OnTriggerEnter with: " + other.name);
// //
// //         if (other.name == "Player")
// //         {
// //             player_detection = true;
// //             Debug.Log("Player detected: player_detection set to true.");
// //         }
// //     }
// //
// //     private void OnTriggerExit(Collider other)
// //     {
// //         Debug.Log("OnTriggerExit with: " + other.name);
// //
// //         if (other.name == "Player")
// //         {
// //             player_detection = false;
// //             Debug.Log("Player exited: player_detection set to false.");
// //         }
// //     }
// // }
//
// using UnityEngine;
// using TMPro;
//
// public class NPCsystem : MonoBehaviour
// {
//     [Header("Dialogue Settings")]
//     public GameObject d_template;  // The dialogue prefab
//     public Transform canva;        // The Canvas or panel to parent new lines
//
//     private bool player_detection = false;
//
//     // An array of lines you want to display in order
//     private string[] lines = { "hey", "What's good", "Karan has done no work :p" };
//     private int currentIndex = 0;
//
//     void Update()
//     {
//         // If the player is in range and presses E
//         if (player_detection && Input.GetKeyDown(KeyCode.E))
//         {
//             // If dialogue isn't active yet, start it
//             if (!MainPlayerMovementScript.dialouge)
//             {
//                 Debug.Log("Starting dialogue...");
//                 // Show the UI
//                 canva.gameObject.SetActive(true);
//
//                 // Lock movement
//                 MainPlayerMovementScript.dialouge = true;
//
//                 // Reset to the first line
//                 currentIndex = 0;
//
//                 // Spawn the first line
//                 NewDialogue(lines[currentIndex]);
//             }
//             else
//             {
//                 // Dialogue is already active, so show the next line
//                 currentIndex++;
//                 if (currentIndex < lines.Length)
//                 {
//                     NewDialogue(lines[currentIndex]);
//                 }
//                 else
//                 {
//                     // No more lines left
//                     Debug.Log("No more lines. Ending dialogue.");
//                     canva.gameObject.SetActive(false);
//                     MainPlayerMovementScript.dialouge = false;
//                 }
//             }
//         }
//     }
//
//     /// <summary>
//     /// Instantiates the dialogue prefab and sets the text.
//     /// </summary>
//     void NewDialogue(string text)
//     {
//         Debug.Log("NewDialogue called with text: " + text);
//
//         // Instantiate the dialogue prefab under the canvas
//         GameObject template_clone = Instantiate(d_template, canva);
//         Debug.Log("Instantiated dialogue prefab under canvas.");
//
//         // Check the prefab's children for a TextMeshProUGUI at child(1)
//         if (template_clone.transform.childCount > 1)
//         {
//             TextMeshProUGUI tmp = template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
//             if (tmp != null)
//             {
//                 tmp.text = text;
//                 Debug.Log("Assigned text to prefab child: " + text);
//             }
//             else
//             {
//                 Debug.LogWarning("Child(1) does not have a TextMeshProUGUI component.");
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Dialogue prefab does not have a second child for text.");
//         }
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log("OnTriggerEnter with: " + other.name);
//         if (other.name == "Player")
//         {
//             player_detection = true;
//             Debug.Log("Player in range. player_detection = true.");
//         }
//     }
//
//     private void OnTriggerExit(Collider other)
//     {
//         Debug.Log("OnTriggerExit with: " + other.name);
//         if (other.name == "Player")
//         {
//             player_detection = false;
//             Debug.Log("Player left range. player_detection = false.");
//         }
//     }
// }
//     

using UnityEngine;
using TMPro;

public class NPCsystem : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject d_template;  // The dialogue prefab
    public Transform canva;        // The Canvas or panel to parent new lines

    private bool player_detection = false;

    // An array of lines you want to display in order
    private string[] lines = { "hey", "What's good", "I love you :)" };
    private int currentIndex = 0;

    void Update()
    {
        // If the player is in range and presses E
        if (player_detection && Input.GetKeyDown(KeyCode.E))
        {
            // If dialogue isn't active yet, start it
            if (!MainPlayerMovementScript.dialouge)
            {
                Debug.Log("Starting dialogue...");
                // Show the UI
                if (canva != null)
                {
                    canva.gameObject.SetActive(true);
                }

                // Lock movement
                MainPlayerMovementScript.dialouge = true;

                // Reset to the first line
                currentIndex = 0;

                // Spawn the first line
                SpawnLine(lines[currentIndex]);
            }
            else
            {
                // Dialogue is already active, so show the next line
                currentIndex++;
                if (currentIndex < lines.Length)
                {
                    SpawnLine(lines[currentIndex]);
                }
                else
                {
                    // No more lines left
                    XPManager.Instance.AddXP(30);
                    Debug.Log("NPC interacted with! 30 XP awarded.");
                    if (canva != null)
                    {
                        canva.gameObject.SetActive(false);
                    }
                    MainPlayerMovementScript.dialouge = false;
                }
            }
        }
    }

    private void SpawnLine(string text)
    {
        Debug.Log("Spawning line: " + text);

        if (d_template == null || canva == null)
        {
            Debug.LogWarning("d_template or canva not assigned in NPCsystem!");
            return;
        }

        // Instantiate the dialogue prefab under the canvas
        GameObject template_clone = Instantiate(d_template, canva);
        // Force the new clone to be active, in case the prefab is disabled by default
        template_clone.SetActive(true);

        // Attempt to get the second child
        if (template_clone.transform.childCount > 1)
        {
            TextMeshProUGUI tmp = template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = text;
                Debug.Log("Assigned text to prefab child: " + text);
            }
            else
            {
                Debug.LogWarning("Child(1) does not have a TextMeshProUGUI component.");
            }
        }
        else
        {
            Debug.LogWarning("Dialogue prefab does not have a second child for text.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detection = true;
            Debug.Log("Player in range. player_detection = true.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            player_detection = false;
            Debug.Log("Player left range. player_detection = false.");
        }
    }
}
