using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 6f;
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (distance <= interactDistance)
            {
                animator.SetTrigger("doorMotion");
            }
        }
    }
}
