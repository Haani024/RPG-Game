using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePageScript : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    private string[] messages = { "Born under an ill-omened blood-red eclipse, you are marked by fate with ashen-white eyes and blackened veins—signs of an ancient and terrible curse. " +
                                  "Feared by your village, you are cast out into a world that sees you as a monster. With nowhere to turn, you wanders the wilds, a land filled with prowling beasts " +
                                  "and lawless murderers, searching for answers about your affliction. Legends speak of a way to break this curse, ancient gods that guard the secrets of fate. " +
                                  "Only by hunting them down and claiming their unique abilities will you gain the knowledge and strength to find those who did this to you." +
                                  "\n\n Press Enter to Continue..." };
    private int currentMessageIndex = -1;
    private bool isTyping = false;
    public string nextclass = "Customization";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            currentMessageIndex++;
            if (currentMessageIndex < messages.Length)
            {
                StartCoroutine(TypeText(messages[currentMessageIndex]));
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        titleText.text = "";
        foreach (char letter in message)
        {
            titleText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextclass);
    }
}
