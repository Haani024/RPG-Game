using UnityEngine;

public class PlayerSwimController : MonoBehaviour
{
    public float swimSpeed = 3f;
    public float waterDrag = 1f;
    public float gravityScale = 1f;

    private Rigidbody rb;
    private bool isInWater = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isInWater)
        {
            SwimMovement();
        }
        else
        {
            // Your normal movement code here
        }
    }

    private void SwimMovement()
    {
      Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") > 0f ? 1 : 0, Input.GetAxis("Vertical"));

        Vector3 movement = input.normalized * swimSpeed;

        rb.linearVelocity = movement;

        // Optional: reduce gravity and add drag for a floaty feel
        rb.linearDamping = waterDrag;
        rb.useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            rb.useGravity = true;
            rb.linearDamping = 0;
        }
    }
}
