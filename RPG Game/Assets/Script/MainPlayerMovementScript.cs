using UnityEngine;

public class MainPlayerMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sprintSpeed = 10f;
    public float horizontalMovementMultiplier = 0.5f;
    public float mouseSensitivity = 3f;
    public float movementThreshold = 0.05f;
    public float gravity = 9.81f;  // Gravity force
    public float groundCheckDistance = 0.2f; // Distance to check for ground
    private Vector3 velocity; // Stores gravity force

    [Header("Camera Reference")]
    public Camera mainCamera;

    private CharacterController controller;
    private Animator animator;
    private float currentRotationX = 0f;

    // Global flag to lock movement
    public static bool dialouge = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Ensure we have a camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Initially lock the cursor (FPS style)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // If dialogue is active, disable movement & show cursor
        if (dialouge)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (animator != null)
            {
                animator.SetBool("IsSprinting", false);
            }

            // Skip movement logic
            return;
        }
        else
        {
            // Normal locked-cursor mode
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // ----- Movement & Rotation Logic (only runs if dialogue == false) -----

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold
            ? Input.GetAxisRaw("Horizontal")
            : 0f;
        float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold
            ? Input.GetAxisRaw("Vertical")
            : 0f;

        Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier)
                              + transform.forward * vertical;
        moveDirection = moveDirection.normalized;

        bool isMoving = moveDirection.magnitude > 0;

        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsSprinting", vertical > 0);
            animator.SetBool("MovingB", vertical < 0);
            animator.SetBool("MovingL", horizontal > 0);
            animator.SetBool("MovingR", horizontal < 0);
        }

        // Apply movement
        if (isMoving)
        {
            controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
        }

        // Apply gravity manually
        if (!IsGrounded())
        {
            velocity.y -= gravity * Time.deltaTime; // Apply gravity over time
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f; // Small reset value to avoid continuous falling
        }

        controller.Move(velocity * Time.deltaTime); // Apply gravity to movement

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (mainCamera == null) return;

        // A simple 3rd-person camera
        Vector3 targetPosition = transform.position
            + transform.forward * -5f
            + Vector3.up * 2f;

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

