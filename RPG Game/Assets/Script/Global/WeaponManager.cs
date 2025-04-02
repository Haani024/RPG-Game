using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    public int SelectedWeaponIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSelectedWeapon();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedWeapon(int index)
    {
        SelectedWeaponIndex = index;
        PlayerPrefs.SetInt("SelectedWeapon", index);
        PlayerPrefs.Save();
    }

    public void LoadSelectedWeapon()
    {
        SelectedWeaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);
    }
}