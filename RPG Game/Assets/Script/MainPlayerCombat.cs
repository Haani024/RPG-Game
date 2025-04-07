using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public WeaponContactDetection sword;

    private int comboCount = 0;
    private bool comboQueued = false;
    private bool isAttacking = false;

    // Adjust this threshold to control when input is accepted during an attack.
    private float comboInputThreshold = 0.7f; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animator.applyRootMotion = true;
    }

    void Update()
    {
        // Check for attack input every frame
        if (Input.GetMouseButtonDown(1))
        {
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // If already in an attack, check if we're past the threshold to queue the next combo
        if (stateInfo.IsTag("Attack"))
        {
            if (stateInfo.normalizedTime >= comboInputThreshold && !comboQueued)
            {
                comboQueued = true;
            }
        }
        else
        {
            // If not attacking, start the attack immediately
            StartAttack();
        }
    }

    private void StartAttack()
    {
        // Cycle through combo attacks (assuming three distinct attack animations)
        comboCount = (comboCount % 3) + 1;
        animator.ResetTrigger("NoAttack");
        animator.SetTrigger("Attack" + comboCount);
        isAttacking = true;
        comboQueued = false;
    }

    
   

    // This method applies root motion to the character during attacks.
    private void OnAnimatorMove()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            controller.Move(animator.deltaPosition);
            transform.rotation *= animator.deltaRotation;
        }
    }
    public void AttackAniEnd()
    {
        if (comboQueued)
        {
            // If an attack input was queued, immediately start the next attack
            comboQueued = false;
            StartAttack();
        }
        else
        {
            // No follow-up attack; reset the combo state.
            animator.SetTrigger("NoAttack");
            comboCount = 0;
            isAttacking = false;
        }
    }

    // Called via animation events to manage the sword's hitbox.
    public void ActivateSwordHitbox()
    {
        sword.StartAttackCollider();
    }

    public void DeactivateSwordHitbox()
    {
        sword.EndAttackCollider();
    }
}
