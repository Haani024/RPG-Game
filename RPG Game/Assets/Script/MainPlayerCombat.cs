using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public WeaponContactDetection sword;

    private int comboCount = 0;
    private bool comboQueued = false;
    private bool isAttacking = false;

    
    private float comboInputThreshold = 0.7f; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animator.applyRootMotion = true;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        
        if (stateInfo.IsTag("Attack"))
        {
            if (stateInfo.normalizedTime >= comboInputThreshold && !comboQueued)
            {
                comboQueued = true;
            }
        }
        else
        {
            
            StartAttack();
        }
    }

    private void StartAttack()
    {
        
        comboCount = (comboCount % 3) + 1;
        animator.ResetTrigger("NoAttack");
        animator.SetTrigger("Attack" + comboCount);
        isAttacking = true;
        comboQueued = false;
    }

    
   

    
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
            
            comboQueued = false;
            StartAttack();
        }
        else
        {
            
            animator.SetTrigger("NoAttack");
            comboCount = 0;
            isAttacking = false;
        }
    }

    
    public void ActivateSwordHitbox()
    {
        sword.StartAttackCollider();
    }

    public void DeactivateSwordHitbox()
    {
        sword.EndAttackCollider();
    }
}
