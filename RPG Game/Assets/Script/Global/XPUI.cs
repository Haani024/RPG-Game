using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    public Text levelText;
    public Text xpText;
    public Slider xpSlider;

    private void Start()
    {
        if (XPManager.Instance == null)
        {
            Debug.LogError("XPManager.Instance is null! Make sure XPManager exists in the scene.");
            return;
        }

        UpdateXPUI(XPManager.Instance.currentXP, XPManager.Instance.currentLevel);

        XPManager.Instance.OnXPUpdated += UpdateXPUI;
    }

    private void OnDestroy()
    {
        if (XPManager.Instance != null)
            XPManager.Instance.OnXPUpdated -= UpdateXPUI;
    }

    private void UpdateXPUI(int xp, int level)
    {
        levelText.text = "Level: " + level;
        xpText.text = $"{xp} / {XPManager.Instance.xpToNextLevel} XP";
        xpSlider.maxValue = XPManager.Instance.xpToNextLevel;
        xpSlider.value = xp;
    }
}
