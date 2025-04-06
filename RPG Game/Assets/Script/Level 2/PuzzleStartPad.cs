using UnityEngine;

public class PuzzleStartPad : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    private bool hasStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            hasStarted = true;
            puzzleManager.StartPuzzle();
            Debug.Log("Puzzle started from start pad!");
        }
    }
}