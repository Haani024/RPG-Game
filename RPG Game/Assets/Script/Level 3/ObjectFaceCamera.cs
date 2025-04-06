using UnityEngine;

public class ObjectFaceCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}
