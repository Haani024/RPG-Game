using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public int requiredPresses = 3;    // The number of times the plate needs to be pressed
    private int currentPresses = 0;    // The current number of presses on this plate

    public static bool[] allPlatesActivated = new bool[4]; // Array to track if all plates are activated
    private static int activatedPlatesCount = 0;  // Count of activated plates

    public GameObject gate;           // Reference to the gate object to unlock
    public Animator gateAnimator;     // Reference to the gate's animator for opening

    private bool isPressed = false;   // Tracks if this plate is currently pressed
    private bool canRotate = true;    // Flag to control if the pipe can still rotate

    public PipeRotator pipeRotator;  // Reference to the PipeRotator component
    public float rotationSpeed = 90f; // Speed of pipe rotation

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the plate and rotation is still allowed
        if (other.CompareTag("Player") && !isPressed && canRotate)
        {
            isPressed = true;
            currentPresses++;

            // Rotate the pipe by calling the RotatePipe method from PipeRotator
            if (pipeRotator != null)
            {
                pipeRotator.RotatePipe();
            }
            else
            {
                Debug.LogWarning("PipeRotator component is not assigned.");
            }

            // If the required presses are reached, stop rotation and activate the plate
            if (currentPresses >= requiredPresses)
            {
                // Disable further rotations
                canRotate = false;

                // Mark this plate as activated
                allPlatesActivated[activatedPlatesCount] = true;
                activatedPlatesCount++;

                // Check if all plates are activated
                if (activatedPlatesCount == allPlatesActivated.Length)
                {
                    UnlockGate();
                }
            }

            Debug.Log("Plate Pressed: " + currentPresses + "/" + requiredPresses);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player exits the plate, allow it to be pressed again
        if (other.CompareTag("Player"))
        {
            isPressed = false;
        }
    }

    // Unlock the gate when all plates have been activated
    void UnlockGate()
    {
        if (gateAnimator != null)
        {
            gateAnimator.SetTrigger("bigdoor"); // Trigger the gate's opening animation
            Debug.Log("Gate Unlocked!");
            XPManager.Instance.AddXP(100);
        }
        else
        {
            Debug.LogError("Gate Animator not assigned!");
        }
    }

    private void ResetPressurePlates()
    {
        // Reset the plate tracking variables
        activatedPlatesCount = 0;
        for (int i = 0; i < allPlatesActivated.Length; i++)
        {
            allPlatesActivated[i] = false;
        }
    }

    private void Start()
    {
        // Reset the plate status when the scene starts (in case it's used in multiple levels or scenes)
        ResetPressurePlates();
    }
}
