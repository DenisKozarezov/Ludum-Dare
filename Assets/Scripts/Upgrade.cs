using System;
using Core.Services;
using Core.Units;
using UnityEngine;
public class Upgrade : IComparable<Upgrade>
{
    public Operator operatorType;
    public string parameter;
    public string name;
    public string desc;
    public float value;
    public byte useCount = 0;

    public UnitUpgradeArgs args;

    public byte spawnAmount;
    public byte spawnRate;

    protected EnergyDistribution energyDistribution = null;
    protected UnitsManager unitsManager = null;
    protected WaveManager waveManager = null;

    public Upgrade(EnergyDistribution energyDistribution, string name, string desc, string upgradeType, string parameter, float value)
    {
        operatorType = OperatorFactory.CreateOperator(upgradeType);
        this.energyDistribution = energyDistribution;
        this.name = name;
        this.desc = desc;
        this.parameter = parameter;
        this.value = value;
    }

    public Upgrade(UnitsManager unitsManager, string name, string desc, UnitUpgradeArgs args)
    {
        this.unitsManager = unitsManager;
        this.name = name;
        this.desc = desc;
        this.args = args;
    }

    public Upgrade(WaveManager waveManager, string name, string desc, byte spawnAmount, byte spawnRate)
    {
        this.waveManager = waveManager;
        this.name = name;
        this.desc = desc;
        this.spawnAmount = spawnAmount;
        this.spawnRate = spawnRate;
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
        Debug.Log("upgraded energy");
        useCount++;
    }
    
    public void UpgradeValue(ref UnitsManager entity)
    {
        entity.UpgradeAllAliveFriendlyUnits(args);
        Debug.Log("upgraded units");
        useCount++;
    }

    public void UpgradeValue(ref WaveManager entity)
    {
        entity.UpgradeSpawners(spawnAmount, spawnRate);
        Debug.Log("upgraded spawner");
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
        else if (waveManager != null)
        {
            this.UpgradeValue(ref waveManager);
        }
    }
}