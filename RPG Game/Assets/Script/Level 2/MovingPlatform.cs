using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    private Vector3 target;
    private Vector3 lastPosition;
    private Vector3 movementDelta;

    private void Start()
    {
        target = pointB;
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Move the platform
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        // Calculate how far the platform moved this frame
        movementDelta = transform.position - lastPosition;

        lastPosition = transform.position;

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    public Vector3 GetMovementDelta()
    {
        return movementDelta;
    }
}




