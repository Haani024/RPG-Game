using UnityEngine;

public class waveObstacle : MonoBehaviour
{
    [Header("Wave Movement Settings")]
    public float speed = 5f;         // Speed of wave movement
    public float moveDistance = 10f; // Distance the wave moves diagonally
    private Vector3 startPos;        // Stores the initial position
    private int direction = 1;       // 1 = forward, -1 = backward
    private Vector3 moveDirection;   // 45-degree diagonal movement

    [Header("Wave Damage Settings")]
    public int damageAmount = 10;  // Damage to apply per hit
    public float damageCooldown = 1f; // Cooldown before damaging again
    private float lastDamageTime = 0f; // Tracks last damage time

    void Start()
    {
        startPos = transform.position;
        moveDirection = new Vector3(1, 0, 1).normalized; // Move diagonally
    }

    void Update()
    {
        // Move the wave continuously
        transform.position += moveDirection * speed * direction * Time.deltaTime;

        // Reverse direction when reaching movement limits
        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            lastDamageTime = Time.time; // Update the cooldown timer

            HealthSystem.Instance.TakeDamage(50);
        }
    }
}
