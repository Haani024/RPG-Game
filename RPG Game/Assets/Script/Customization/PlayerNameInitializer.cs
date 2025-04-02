using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInitializer : MonoBehaviour
{
    public InputField nameInputField;
    public Button startButton;

    public static string PlayerName { get; private set; } 
    private void Start()
    {
        nameInputField.text = "";

        startButton.onClick.AddListener(SavePlayerName);
    }

    private void SavePlayerName()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerName = playerName;
            PlayerPrefs.SetString("PlayerName", PlayerName);
            PlayerPrefs.Save();

            Debug.Log("Player name saved: " + PlayerName);
        }
        else
        {
            Debug.LogWarning("Player name is empty! Please enter a name.");
        }
    }
}
