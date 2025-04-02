using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public int SelectedcharacterIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSelectedcharacter();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedcharacter(int index)
    {
        SelectedcharacterIndex = index;
        PlayerPrefs.SetInt("Selectedcharacter", index);
        PlayerPrefs.Save();
    }

    public void LoadSelectedcharacter()
    {
        SelectedcharacterIndex = PlayerPrefs.GetInt("Selectedcharacter", 0);
    }
}
