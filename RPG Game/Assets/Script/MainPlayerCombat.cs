using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public WeaponContactDetection sword;

    private int comboCount = 0;
    private float comboTimer = 0f;
    private float comboWindow = 1.5f;
    private bool comboQueued = false;
    private bool isComboLocked = false; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null) return; // still not ready
        }

        HandleComboTimer();
        HandleAttackInput();
    }


    private void HandleComboTimer()
    {
        if (comboCount > 0 && !isComboLocked)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboWindow)
            {
                ResetCombo();
            }
        }
    }

    private void HandleAttackInput()
    {
        if (isComboLocked) return; 

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(1))
        {
            if (stateInfo.IsTag("Attack") && stateInfo.normalizedTime >= 0.7f)
            {
                if (!comboQueued)
                {
                    comboQueued = true;

                    if (comboCount == 2) 
                    {
                        isComboLocked = true; 
                    }
                }
            }
            else if (!stateInfo.IsTag("Attack"))
            {
                PerformAttack();
            }
        }
    }

    private void PerformAttack()
    {
        comboTimer = 0f;
        comboCount = (comboCount % 3) + 1;

        animator.ResetTrigger("NoAttack");
        animator.SetTrigger($"Attack{comboCount}");
    }

    private void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0f;
        comboQueued = false;
        isComboLocked = false;

        animator.SetTrigger("NoAttack");
    }

    private void OnAnimatorMove()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsTag("Attack"))
        {
            controller.Move(animator.deltaPosition);
            transform.rotation *= animator.deltaRotation;

            
            if (comboQueued && stateInfo.normalizedTime >= 0.85f)
            {
                comboQueued = false;
                PerformAttack();
            }

           
            if (isComboLocked && comboCount == 3 && stateInfo.normalizedTime >= 0.95f)
            {
                ResetCombo();
            }
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
