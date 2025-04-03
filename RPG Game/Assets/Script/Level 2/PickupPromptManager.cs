using UnityEngine;

public class PickupPromptManager : MonoBehaviour
{
    public GameObject globalPickupPrompt;
    
    public static PickupPromptManager instance;
    
    void Awake()
    {
        // Setup singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
            
        // Hide prompt on start
        if (globalPickupPrompt != null)
            globalPickupPrompt.SetActive(false);
        
        
    }
    
    public void ShowPrompt(bool show)
    {
        if (globalPickupPrompt != null)
            globalPickupPrompt.SetActive(show);
    }
    
    

    
}