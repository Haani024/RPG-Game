using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sprintSpeed = 10f;
    public float horizontalMovementMultiplier = 0.5f;
    public float mouseSensitivity = 3f;
    public float movementThreshold = 0.05f;
    public float jumpHeight = 100.0f;
    public float gravityValue = -9.81f;
    public static bool dialouge = false;

    [Header("Camera Reference")]
    public Camera mainCamera;

    private CharacterController controller;
    private Animator animator;
    private float currentRotationX = 0f;
    private Vector3 playerVelocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // Get input with movement threshold
        float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold
            ? Input.GetAxisRaw("Horizontal")
            : 0f;
        float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold
            ? Input.GetAxisRaw("Vertical")
            : 0f;

        // Calculate movement direction
        Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier) +
                                transform.forward * vertical;
        moveDirection = moveDirection.normalized;

        bool isMoving = moveDirection.magnitude > 0;
        bool groundedPlayer = controller.isGrounded;

        // Animator Updates
        if (animator != null)
        {
            animator.SetBool("IsSprinting", isMoving);
            animator.SetBool("IsJumping", !groundedPlayer);
            animator.SetBool("IsGrounded", groundedPlayer);
        }

        // Update horizontal movement velocity
        if (isMoving)
        {
            playerVelocity.x = moveDirection.x * sprintSpeed;
            playerVelocity.z = moveDirection.z * sprintSpeed;
        }
        else
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }

        // Jumping
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (Input.GetButtonDown("Jump") && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

        // Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Move
        controller.Move(playerVelocity * Time.deltaTime);

        // Camera follow
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (mainCamera == null) return;

        Vector3 targetPosition = transform.position
            + transform.forward * -5f
            + Vector3.up * 2f;

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
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
