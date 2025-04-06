using UnityEngine;

public class FloorPadTrigger : MonoBehaviour
{
    public int colorIndex; // 0 = green, 1 = yellow, 2 = black
    public PuzzleManager puzzleManager;

    private bool playerOnPad = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerOnPad)
        {
            playerOnPad = true;
            puzzleManager.PlayerStep(colorIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPad = false;
        }
    }
}




