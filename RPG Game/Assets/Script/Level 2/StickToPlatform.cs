using UnityEngine;



public class StickToPlatform : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("MovingPlatform"))
        {
            if (transform.parent != hit.transform)
            {
                Debug.Log("Parented to platform: " + hit.gameObject.name);
                transform.SetParent(hit.transform);
            }
        }
        else
        {
            if (transform.parent != null)
            {
                Debug.Log("Unparented from platform");
                transform.SetParent(null);
            }
        }
    }
}

