using UnityEngine;

public class FloorPadTrigger : MonoBehaviour
{
    public Color padColor;
    public PuzzleManager puzzleManager;

    private bool playerOnPad = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerOnPad)
        {
            puzzleManager.PlayerStep(padColor);
            playerOnPad = true;
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



