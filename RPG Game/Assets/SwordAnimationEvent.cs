using UnityEngine;

public class SwordAnimationEvent : MonoBehaviour
{
    private MainPlayerCombat playerCombat;

    void Start()
    {
        
        playerCombat = GetComponentInParent<MainPlayerCombat>();
    }

    
    public void ActivateSwordHitbox()
    {
        if (playerCombat != null)
            playerCombat.ActivateSwordHitbox();
    }

    public void DeactivateSwordHitbox()
    {
        if (playerCombat != null)
            playerCombat.DeactivateSwordHitbox();
    }
}

