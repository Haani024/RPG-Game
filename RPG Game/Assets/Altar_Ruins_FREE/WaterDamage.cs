using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    public int damagePerSecond = 10;
    private bool playerInWater = false;
    private PlayerHealth playerHealth;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = true;
            playerHealth = other.GetComponent<PlayerHealth>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = false;
            playerHealth = null;
        }
    }

    void Update()
    {
        if (playerInWater && playerHealth != null)
        {
            playerHealth.TakeDamage((int)(damagePerSecond * Time.deltaTime));

        }
    }
}