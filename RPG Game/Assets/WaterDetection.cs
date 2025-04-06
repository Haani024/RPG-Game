using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    public CharacterController controller;
    public float gravityValue = -9.81f;
    public float waterGravity = -2f;
    private Vector3 playerVelocity;
    private bool isInWater = false;

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player entered water");
            isInWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player left water");
            isInWater = false;
        }
    }
}