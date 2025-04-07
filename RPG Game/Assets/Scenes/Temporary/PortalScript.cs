using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int targetLevelIndex;  // e.g., 1 = Level1, 2 = Level2
    public string sceneName;      // scene name to load

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.name); // Add this for quick debugging

        if (other.CompareTag("Player"))
        {
            if (LevelUnlock.Instance.IsLevelUnlocked(targetLevelIndex))
            {
                Debug.Log("Portal entered, loading scene: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Level " + targetLevelIndex + " is locked!");
            }
        }
    }
}
