using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private int comboCount = 0;
    private float comboTimer = 0f;
    private float comboWindow = 1f;

    public bool canAttack = false; // <-- Add this
	public GameObject equippedSword; 
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!canAttack) return; // <- Don't allow attacks until pickup

        if (comboCount > 0)
        {
            comboTimer += Time.deltaTime;

            if (comboTimer > comboWindow)
            {
                ResetCombo();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        comboTimer = 0f;
        comboCount++;

        switch (comboCount)
        {
            case 1:
                animator.SetTrigger("Attack1");
                TryHitEnemy();
                break;
            case 2:
                animator.SetTrigger("Attack2");
                TryHitEnemy();
                break;
            case 3:
                animator.SetTrigger("Attack3");
                TryHitEnemy();
                break;
            default:
                ResetCombo();
                break;
        }
    }

    void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0f;
        animator.SetTrigger("NoAttack");
    }

    void TryHitEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(25f);
                }
            }
        }
    }

    // Call this from pickup
    public void EnableCombat()
    {
        canAttack = true;

        if (equippedSword != null)
        {
            equippedSword.SetActive(true); // 👈 show the sword
        }

        Debug.Log("Combat unlocked!");
    }
}