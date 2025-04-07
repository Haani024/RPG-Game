using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    public static LevelUnlock Instance;

    private const string ProgressKey = "LevelProgress";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 🔐 Ensure Level 1 is always unlocked on first play
            if (!PlayerPrefs.HasKey(ProgressKey))
            {
                PlayerPrefs.SetInt(ProgressKey, 1); // Unlock Level 1
                PlayerPrefs.Save();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Get the highest unlocked level (default to 1 if none saved)
    public int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(ProgressKey, 1);
    }

    // Unlocks the next level after the current one
    public void UnlockNextLevel(int completedLevel)
    {
        int current = GetUnlockedLevel();
        if (completedLevel >= current)
        {
            PlayerPrefs.SetInt(ProgressKey, completedLevel + 1);
            PlayerPrefs.Save();
        }
    }

    // Checks if the specified level is unlocked
    public bool IsLevelUnlocked(int level)
    {
        return level <= GetUnlockedLevel();
    }

    // Resets progress back to Level 1
    public void ResetProgress()
    {
        PlayerPrefs.SetInt(ProgressKey, 1);
        PlayerPrefs.Save();
    }
}
