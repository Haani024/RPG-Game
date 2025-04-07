using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell. Respawning...");

            // 👇 Unparent the player from any moving platform
            other.transform.SetParent(null);

            // 👇 Reset position
            other.transform.position = respawnPoint.position;
        }
    }
}