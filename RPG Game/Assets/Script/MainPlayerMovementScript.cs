using System;
using UnityEngine;

public class MainPlayerMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sprintSpeed = 10f;
    public float horizontalMovementMultiplier = 0.5f;
    public float mouseSensitivity = 3f;
    public float movementThreshold = 0.05f;
    public float gravity = 9.81f;
    public float groundCheckDistance = 0.2f;

    private Vector3 velocity;
    public float jumpHeight = 1.5f;
    public float jumpCooldown = 0.2f;
    private float lastJumpTime = -10f;
    private bool isJumping = false;

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

        // Delay Animator assignment in case model is swapped in late
        Invoke(nameof(RefreshAnimator), 0.1f);

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RefreshAnimator()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator not found! Make sure the model prefab has one.");
        }
    }

    void Update()
    {
        if (dialouge)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (animator != null)
            {
                animator.SetBool("IsSprinting", false);
            }

            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && Time.time > lastJumpTime + jumpCooldown)
        {
            velocity.y = Mathf.Sqrt(8f * jumpHeight * gravity);
            lastJumpTime = Time.time;
            isJumping = true;
        }

        if (IsGrounded() && isJumping && velocity.y < 0)
        {
            isJumping = false;
        }

        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsSprinting", vertical > 0);
            animator.SetBool("MovingB", vertical < 0);
            animator.SetBool("MovingL", horizontal > 0);
            animator.SetBool("MovingR", horizontal < 0);
            animator.SetBool("IsJumping", isJumping);
        }

        if (isMoving)
        {
            controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
        }

        // Gravity
        if (!IsGrounded())
        {
            velocity.y -= 2 * gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (mainCamera == null) return;

        Vector3 targetPosition = transform.position + transform.forward * -5f + Vector3.up * 2f;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * 5f);
        mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    bool IsGrounded()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayStart, Vector3.down, groundCheckDistance + 0.1f))
        {
            return true;
        }

        if (controller != null && controller.isGrounded)
        {
            return true;
        }

        return false;
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

