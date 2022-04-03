using UnityEngine;

public class ButtonListener : MonoBehaviour
{
    public Upgrade upgrade;
    public EnergyDistribution energyDistribution;
    [SerializeField] string operatorType;
    [SerializeField] string parameter;
    [SerializeField] float parameterValue;

    public void createUpgrade()
    {
        upgrade = new Upgrade("Upgrade name", "Upgrade desc", operatorType, parameter, parameterValue);
        upgrade.UpgradeValue(ref energyDistribution);
    }
}
