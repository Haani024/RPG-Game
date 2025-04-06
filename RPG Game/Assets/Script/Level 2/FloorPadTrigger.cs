using UnityEngine;

public class FloorPadTrigger : MonoBehaviour
{
    public int padIndex;
    public PuzzleManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.PlayerStep(padIndex);
        }
    }
}

