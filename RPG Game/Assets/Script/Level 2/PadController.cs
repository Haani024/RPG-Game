using UnityEngine;


using System.Collections;

public class PadController : MonoBehaviour
{
    private Color originalColor;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public IEnumerator Flash()
    {
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.6f);
        rend.material.color = originalColor;
    }

    public void ResetPad()
    {
        rend.material.color = originalColor;
    }
}

