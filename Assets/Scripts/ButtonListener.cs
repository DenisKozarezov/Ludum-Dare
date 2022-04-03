using UnityEngine;
using Core.Services;

public class ButtonListener : MonoBehaviour
{
    private Upgrade upgrade;

    public void getUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
    }

    public void createUpgrade()
    {
        upgrade.UpgradeValue();
    }
}
