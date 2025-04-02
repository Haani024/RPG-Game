
using UnityEngine;
using TMPro;

public class NPCsystem : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject d_template;  // The dialogue prefab
    public Transform canva;        // The Canvas or panel to parent new lines

    private bool player_detection = false;

   [Header("Dialogue Lines")]
   [SerializeField] 
   private string[] lines;
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
