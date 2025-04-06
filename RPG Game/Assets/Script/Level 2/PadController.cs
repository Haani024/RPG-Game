using UnityEngine;

public class PadController : MonoBehaviour
{
    private Renderer rend;
    public static readonly Color GreyColor = Color.gray;

    private Color currentColor = GreyColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        ResetPad();
    }

    // Used to flash the sequence color during the show phase
    public void ShowSequenceColor(Color color)
    {
        rend.material.color = color;
        currentColor = color;
    }

    // Used after show phase to hide the color
    public void ResetPad()
    {
        currentColor = GreyColor;
        rend.material.color = GreyColor;
    }

    // Called when player selects a color
    public void SetPlayerColor(Color color)
    {
        currentColor = color;
        rend.material.color = color;
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }
}



