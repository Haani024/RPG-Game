using UnityEngine;
using System;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 100;

    public event Action<int, int> OnXPUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        OnXPUpdated?.Invoke(currentXP, currentLevel);

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);

        Debug.Log($"Leveled Up! New Level: {currentLevel}");
        OnXPUpdated?.Invoke(currentXP, currentLevel);
    }
}

