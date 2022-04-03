public class Upgrade
{
    public EnergyDistribution energyDistribution;
    public Operator operatorType;
    public string parameter;
    public float value;
    public byte useCount = 0;

    public Upgrade(string upgradeType, string parameter, float value)
    {
        operatorType = OperatorFactory.CreateOperator(upgradeType);
        this.parameter = parameter;
        this.value = value;
    }

    public void UpgradeValue(ref EnergyDistribution entity)
    {
        float paramValue = (float)entity[parameter];
        entity[parameter] = operatorType.Update(paramValue, value);
        useCount++;
    }

    // public void UpgradeValue(ref EnergyDistribution entity)
    // {
    //     float paramValue = (float)entity[parameter];
    //     entity[parameter] = operatorType.Update(paramValue, value);
    //     useCount++;
    // }

    // public void UpgradeValue(ref EnergyDistribution entity)
    // {
    //     float paramValue = (float)entity[parameter];
    //     entity[parameter] = operatorType.Update(paramValue, value);
    //     useCount++;
    // }
}