using UnityEngine;

public class PipeRotator : MonoBehaviour
{
    // Public variables for rotation on each axis
    public float rotationX = 0f; // Rotation around X-axis
    public float rotationY = 90f; // Rotation around Y-axis
    public float rotationZ = 0f; // Rotation around Z-axis

    // Method to rotate the pipe based on the set values
    public void RotatePipe()
    {
        // Rotate the pipe based on the rotation values on each axis
        transform.Rotate(rotationX, rotationY, rotationZ, Space.Self);
    }
}
