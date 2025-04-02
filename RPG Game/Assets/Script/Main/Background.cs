using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Image uiImage;
    public string firstImageName = "TitlePage.png";
    private int pressCount = 0;

    void Start()
    {
        Sprite firstImage = Resources.Load<Sprite>(firstImageName);
        if (uiImage != null && firstImage != null)
        {
            uiImage.sprite = firstImage;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pressCount++;

            if (pressCount == 1)
            {
                uiImage.color = Color.black;
                uiImage.sprite = null;
            }
        }
    }
}
