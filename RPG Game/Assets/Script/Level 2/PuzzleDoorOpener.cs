using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openSpeed = 2f;

    private bool isOpening = false;

    private Quaternion leftTargetRotation;
    private Quaternion rightTargetRotation;

    void Start()
    {
        // Set the exact final rotations (manually verified)
        leftTargetRotation = Quaternion.Euler(0, -71f, 0);
        rightTargetRotation = Quaternion.Euler(0, 71f, 0);
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
        Debug.Log("Puzzle doors opening!");
        isOpening = true;
    }
}



