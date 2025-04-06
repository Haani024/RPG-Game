using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public float floatStrength = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if boat is below water level
        if (transform.position.y < waterLevel)
        {
            float depth = waterLevel - transform.position.y;
            Vector3 buoyancyForce = Vector3.up * floatStrength * depth;

            rb.AddForce(buoyancyForce, ForceMode.Force);
        }
    }
}