public class Upgrade
{
    public EnergyDistribution energyDistribution;
    public Operator operatorType;
    public string parameter;
    public string name;
    public string desc;
    public float value;
    public byte useCount = 0;

    public Upgrade(string name, string desc, string upgradeType, string parameter, float value)
    {
        operatorType = OperatorFactory.CreateOperator(upgradeType);
        this.parameter = parameter;
        this.value = value;
        this.desc = desc;
        this.name = name;
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