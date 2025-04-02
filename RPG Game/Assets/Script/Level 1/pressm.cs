using UnityEngine;
using UnityEngine.SceneManagement;

public class pressm : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Level 6");
        }
    }
}
