using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack combat = other.GetComponent<Attack>();
            if (combat != null)
            {
                combat.EnableCombat(); // 💥 this line tells the player they picked up the item
                Destroy(gameObject);   // Remove item from scene
            }
        }
    }
}