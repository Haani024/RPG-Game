using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float floatSpeed = 1f;           
    public float floatHeight = 0.5f;        
    public float rotationSpeed = 20f;
    public int xpgranted = 0;
    public int healthgranted = 0;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, newY, 0);

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            XPManager.Instance.AddXP(xpgranted);
            HealthSystem.Instance.Heal(healthgranted);
            Destroy(gameObject);
        }
    }
}
