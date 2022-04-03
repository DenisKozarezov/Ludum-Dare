using System;
using System.Collections.Generic;
using Core.Services;
using UnityEngine;
public class Upgrade : IComparable<Upgrade>
{
    public Operator operatorType;
    public string parameter;
    public string name;
    public string desc;
    public float value;
    public byte useCount = 0;

    protected EnergyDistribution energyDistribution = null;
    protected UnitsManager unitsManager = null;

    public Upgrade(EnergyDistribution energyDistribution, string name, string desc, string upgradeType, string parameter, float value)
    {
        operatorType = OperatorFactory.CreateOperator(upgradeType);
        this.energyDistribution = energyDistribution;
        this.parameter = parameter;
        this.value = value;
        this.desc = desc;
        this.name = name;
    }

    public Upgrade(UnitsManager unitsManager, string name, string desc, string upgradeType, string parameter, float value)
    {
        operatorType = OperatorFactory.CreateOperator(upgradeType);
        this.unitsManager = unitsManager;
        this.parameter = parameter;
        this.value = value;
        this.desc = desc;
        this.name = name;
    }

    public int CompareTo(Upgrade other)
    {
        if (other == null)
        {
            return 1;
        }
        return this.name.CompareTo(other.name);
    }

    public void UpgradeValue(ref EnergyDistribution entity)
    {
        float paramValue = (float)entity[parameter];
        entity[parameter] = operatorType.Update(paramValue, value);
        useCount++;
    }
    
    public void UpgradeValue(ref UnitsManager entity)
    {
        // float paramValue = (float)entity[parameter];
        // entity[parameter] = operatorType.Update(paramValue, value);
        useCount++;
    }

    public void UpgradeValue()
    {
        if (energyDistribution != null) 
        {
            this.UpgradeValue(ref energyDistribution);
        }
        else if (unitsManager != null)
        {
            this.UpgradeValue(ref unitsManager);
        }
    }
}