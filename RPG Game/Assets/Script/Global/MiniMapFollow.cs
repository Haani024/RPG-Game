using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; // Assign your player here

    [Header("Camera Settings")]
    public float height = 50f;              // How high above the player the camera is
    public float orthographicSize = 30f;    // How much area is visible (zoom level)

    private Camera minimapCam;

    void Start()
    {
        minimapCam = GetComponent<Camera>();

        if (minimapCam != null)
        {
            minimapCam.orthographic = true; // Ensure it's orthographic
            minimapCam.orthographicSize = orthographicSize;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Follow the player from above
        transform.position = new Vector3(target.position.x, height, target.position.z);

        // Keep looking straight down
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Update zoom level if changed in Inspector
        if (minimapCam != null)
        {
            minimapCam.orthographicSize = orthographicSize;
        }
    }
}
