using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject[] weaponPrefabs;
    public Transform modelHolder;

    [Header("Weapon Offset")]
    public Vector3 weaponLocalPosition = Vector3.zero;
    public Vector3 weaponLocalRotation = Vector3.zero;
    public Vector3 weaponLocalScale = Vector3.one;

    void Start()
    {
        // --- Character selection ---
        int selectedCharacter = PlayerPrefs.GetInt("Selectedcharacter", 0);
        if (selectedCharacter < 0 || selectedCharacter >= characterPrefabs.Length)
            selectedCharacter = 0;

        // Clear old model
        foreach (Transform child in modelHolder)
            Destroy(child.gameObject);

        // Spawn new model
        GameObject model = Instantiate(characterPrefabs[selectedCharacter], modelHolder);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        // --- Weapon selection ---
        int selectedWeapon = PlayerPrefs.GetInt("SelectedWeapon", 0);
        if (selectedWeapon < 0 || selectedWeapon >= weaponPrefabs.Length)
            selectedWeapon = 0;

        // Find or create weapon holder
        Transform weaponHolder = model.transform.Find("WeaponHolder");

        if (weaponHolder == null)
        {
            Animator anim = model.GetComponent<Animator>();
            if (anim != null)
            {
                Transform hand = anim.GetBoneTransform(HumanBodyBones.RightHand);
                if (hand != null)
                {
                    GameObject holder = new GameObject("WeaponHolder");
                    holder.transform.SetParent(hand);
                    holder.transform.localPosition = Vector3.zero;
                    holder.transform.localRotation = Quaternion.identity;
                    weaponHolder = holder.transform;
                }
            }
        }

        // Equip the weapon
        if (weaponHolder != null)
        {
            GameObject weapon = Instantiate(weaponPrefabs[selectedWeapon], weaponHolder);
            weapon.transform.localPosition = weaponLocalPosition;
            weapon.transform.localEulerAngles = weaponLocalRotation;
            weapon.transform.localScale = weaponLocalScale;
        }
        else
        {
            Debug.LogError("WeaponHolder not found or created. Please check your character setup.");
        }
    }
}
