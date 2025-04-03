using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public PipeRotator connectedPipe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            connectedPipe.RotatePipe();
        }
    }
}
