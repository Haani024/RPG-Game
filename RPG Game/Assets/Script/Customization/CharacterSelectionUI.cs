using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform previewPoint;
    private GameObject currentPreview;

    public int SelectedcharacterIndex
    {
        get { return PlayerManager.Instance.SelectedcharacterIndex; }
        set
        {
            PlayerManager.Instance.SetSelectedcharacter(value);
            UpdatecharacterPreview();
        }
    }

    private void Start()
    {
        LoadSelectedcharacter();
        UpdatecharacterPreview();
    }

    public void Nextcharacter()
    {
        int currentIndex = PlayerPrefs.GetInt("Selectedcharacter", 0);

        currentIndex = (currentIndex + 1) % characterPrefabs.Length;

        PlayerPrefs.SetInt("Selectedcharacter", currentIndex);
        UpdatecharacterPreview();
    }

    private void UpdatecharacterPreview()
    {
        int selectedIndex = PlayerPrefs.GetInt("Selectedcharacter", 0);

        if (selectedIndex < 0 || selectedIndex >= characterPrefabs.Length)
        {
            Debug.LogWarning("Invalid index, resetting to 0");
            selectedIndex = 0;
            PlayerPrefs.SetInt("Selectedcharacter", 0);
        }

        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        currentPreview = Instantiate(characterPrefabs[selectedIndex], previewPoint.position + new Vector3(0, 0, 2f), Quaternion.identity);

        currentPreview.transform.SetParent(previewPoint, false);
        currentPreview.transform.localPosition = Vector3.zero;
        currentPreview.transform.localRotation = Quaternion.Euler(0, 135, 0);
        currentPreview.transform.localScale = Vector3.one * 1.2f;
    }

    private void LoadSelectedcharacter()
    {
        if (!PlayerPrefs.HasKey("Selectedcharacter"))
        {
            PlayerPrefs.SetInt("Selectedcharacter", 0);
        }
        UpdatecharacterPreview();
    }
}