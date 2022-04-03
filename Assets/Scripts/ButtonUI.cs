using UnityEngine;
using TMPro;

public class ButtonUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDesc;
    public TextMeshProUGUI upgradeCount;

    public void getUpgradeText(Upgrade upgrade)
    {
        upgradeName.text = $"{upgrade.name}";
        upgradeDesc.text = $"{upgrade.desc}";
        upgradeCount.text = $"Level: {upgrade.useCount}";
    }
}
