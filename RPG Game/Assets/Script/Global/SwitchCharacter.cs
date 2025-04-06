using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;  // List of all character prefabs
    public Transform modelHolder;          // Reference to where the character prefab goes

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("Selectedcharacter", 0);

        if (selectedIndex < 0 || selectedIndex >= characterPrefabs.Length)
        {
            Debug.LogWarning("Invalid character index — defaulting to 0");
            selectedIndex = 0;
        }

        // Clean out any existing model in ModelHolder
        foreach (Transform child in modelHolder)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the selected character model
        GameObject model = Instantiate(characterPrefabs[selectedIndex], modelHolder);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
    }
}
