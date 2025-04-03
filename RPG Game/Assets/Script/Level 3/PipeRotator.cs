using UnityEngine;

public class PipeRotator : MonoBehaviour
{
    private bool isActive = false;
    public float rotationAngle = 90f; // Degrees to rotate the pipe

    public void RotatePipe()
    {
        if (isActive)
        {
            transform.Rotate(Vector3.up * rotationAngle);
        }
    }

    // You can tie this to the pressure plate later.
    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
