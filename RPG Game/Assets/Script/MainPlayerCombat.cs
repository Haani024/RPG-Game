using UnityEngine;
using UnityEngine.Timeline;
using System.Collections;

public class MainPlayerCombat : MonoBehaviour
{
    private Animator animator;
    private int comboCount = 0;
    private float comboTimer = 0f;
    private float comboWindow = 1f; 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
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
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
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
}