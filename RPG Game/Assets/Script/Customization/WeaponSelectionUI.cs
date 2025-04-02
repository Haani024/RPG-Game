using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject[] WeaponPrefabs;
    public Transform previewPoint;
    private GameObject currentPreview;

    public int SelectedWeaponIndex
    {
        get { return WeaponManager.Instance.SelectedWeaponIndex; }
        set
        {
            WeaponManager.Instance.SetSelectedWeapon(value);
            UpdateWeaponPreview();
        }
    }

    private void Start()
    {
        LoadSelectedWeapon();
        UpdateWeaponPreview();
    }

    public void NextWeapon()
    {
        int currentIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

        currentIndex = (currentIndex + 1) % WeaponPrefabs.Length;

        PlayerPrefs.SetInt("SelectedWeapon", currentIndex);
        UpdateWeaponPreview();
    }

    private void UpdateWeaponPreview()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

        if (selectedIndex < 0 || selectedIndex >= WeaponPrefabs.Length)
        {
            Debug.LogWarning("Invalid index, resetting to 0");
            selectedIndex = 0;
            PlayerPrefs.SetInt("SelectedWeapon", 0);
        }

        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        currentPreview = Instantiate(WeaponPrefabs[selectedIndex], previewPoint.position + new Vector3(0, 0, 2f), Quaternion.identity);

        currentPreview.transform.SetParent(previewPoint, false);
        currentPreview.transform.localPosition = Vector3.zero;
        currentPreview.transform.localRotation = Quaternion.Euler(0, 135, 0);
        currentPreview.transform.localScale = Vector3.one * 1.2f;
    }

    private void LoadSelectedWeapon()
    {
        if (!PlayerPrefs.HasKey("SelectedWeapon"))
        {
            PlayerPrefs.SetInt("SelectedWeapon", 0);
        }
        UpdateWeaponPreview();
    }
}