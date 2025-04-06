using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpening = false;
    private Quaternion leftStartRotation;
    private Quaternion rightStartRotation;
    private Quaternion leftTargetRotation;
    private Quaternion rightTargetRotation;

    void Start()
    {
        // Record initial door rotations
        leftStartRotation = leftDoor.localRotation;
        rightStartRotation = rightDoor.localRotation;

        // Set target rotations for opening outward
        leftTargetRotation = leftStartRotation * Quaternion.Euler(0, -openAngle, 0);
        rightTargetRotation = rightStartRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (isOpening)
        {
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, leftTargetRotation, Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, rightTargetRotation, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoors()
    {
        isOpening = true;
        Debug.Log("Puzzle doors opening!");
    }
}


