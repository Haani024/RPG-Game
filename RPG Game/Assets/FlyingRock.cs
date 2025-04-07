using UnityEngine;

public class FlyingRock : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float horizontalDistance = 5f;
    [SerializeField] private bool startMovingRight = true;
    [SerializeField] private float horizontalSpeedVariation = 1.5f;
    [SerializeField] private float horizontalDistanceVariation = 2f;
    
    [Header("Vertical Movement Settings")]
    [SerializeField] private bool enableVerticalMovement = true;
    [SerializeField] private float verticalSpeed = 3f;
    [SerializeField] private float verticalDistance = 2f;
    [SerializeField] private bool startMovingUp = true;
    [SerializeField] private float verticalSpeedVariation = 1f;
    [SerializeField] private float verticalDistanceVariation = 1.5f;
    
    [Header("Damage Settings")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private bool randomizeDamage = true;
    [SerializeField] private int damageVariation = 5;
    
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 0, 30);
    [SerializeField] private float rotationVariation = 20f;
    
    private Vector3 startPosition;
    private bool movingRight;
    private bool movingUp;
    
    private float leftBoundary;
    private float rightBoundary;
    private float bottomBoundary;
    private float topBoundary;
    
    private float actualHorizontalSpeed;
    private float actualVerticalSpeed;
    private Vector3 actualRotationSpeed;
    private int actualDamage;
    
    void Start()
    {
        startPosition = transform.position;
        
        RandomizeParameters();
        
        leftBoundary = startPosition.x - actualHorizontalDistance;
        rightBoundary = startPosition.x + actualHorizontalDistance;
        
        bottomBoundary = startPosition.y - actualVerticalDistance;
        topBoundary = startPosition.y + actualVerticalDistance;
        
        movingRight = Random.value > 0.5f ? startMovingRight : !startMovingRight;
        movingUp = Random.value > 0.5f ? startMovingUp : !startMovingUp;
    }
    
    private float actualHorizontalDistance;
    private float actualVerticalDistance;
    
    private void RandomizeParameters()
    {
        actualHorizontalSpeed = horizontalSpeed + Random.Range(-horizontalSpeedVariation, horizontalSpeedVariation);
        actualHorizontalDistance = horizontalDistance + Random.Range(-horizontalDistanceVariation, horizontalDistanceVariation);
        
        actualVerticalSpeed = verticalSpeed + Random.Range(-verticalSpeedVariation, verticalSpeedVariation);
        actualVerticalDistance = verticalDistance + Random.Range(-verticalDistanceVariation, verticalDistanceVariation);
        
        if (randomizeDamage)
        {
            actualDamage = damageAmount + Random.Range(-damageVariation, damageVariation + 1);
            actualDamage = Mathf.Max(1, actualDamage);
        }
        else
        {
            actualDamage = damageAmount;
        }
        
        actualRotationSpeed = new Vector3(
            rotationSpeed.x + Random.Range(-rotationVariation, rotationVariation),
            rotationSpeed.y + Random.Range(-rotationVariation, rotationVariation),
            rotationSpeed.z + Random.Range(-rotationVariation, rotationVariation)
        );
    }
    
    void Update()
    {
        float horizontalStep = actualHorizontalSpeed * Time.deltaTime;
        float verticalStep = actualVerticalSpeed * Time.deltaTime;
        
        Vector3 newPosition = transform.position;
        
        if (movingRight)
        {
            newPosition.x = Mathf.MoveTowards(newPosition.x, rightBoundary, horizontalStep);
            
            if (Mathf.Approximately(newPosition.x, rightBoundary))
            {
                movingRight = false;
            }
        }
        else
        {
            newPosition.x = Mathf.MoveTowards(newPosition.x, leftBoundary, horizontalStep);
            
            if (Mathf.Approximately(newPosition.x, leftBoundary))
            {
                movingRight = true;
            }
        }
        
        if (enableVerticalMovement)
        {
            if (movingUp)
            {
                newPosition.y = Mathf.MoveTowards(newPosition.y, topBoundary, verticalStep);
                
                if (Mathf.Approximately(newPosition.y, topBoundary))
                {
                    movingUp = false;
                }
            }
            else
            {
                newPosition.y = Mathf.MoveTowards(newPosition.y, bottomBoundary, verticalStep);
                
                if (Mathf.Approximately(newPosition.y, bottomBoundary))
                {
                    movingUp = true;
                }
            }
        }
        
        transform.position = newPosition;
        
        transform.Rotate(actualRotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }    
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return;
        
        Vector3 center = transform.position;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(center.x - horizontalDistance, center.y, center.z),
            new Vector3(center.x + horizontalDistance, center.y, center.z)
        );
        
        if (enableVerticalMovement)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(center.x, center.y - verticalDistance, center.z),
                new Vector3(center.x, center.y + verticalDistance, center.z)
            );
        }
    }
}