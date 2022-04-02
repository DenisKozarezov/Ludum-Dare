using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    public EnergyDistribution energyDistribution;
    private float income = 0;
    public TextMeshProUGUI energyTextMesh;
    public TextMeshProUGUI percentage;


    void Start()
    {
        income = energyDistribution.energy.incomePerSecond + energyDistribution.energy.incomePerSecond * energyDistribution.energy.efficiency * (energyDistribution.energy.energyPercentage / 100f);
        energyTextMesh.text = String.Format(EnergyText.EnergyCount, energyDistribution.energy.count, energyDistribution.maxTimer, income, energyDistribution.energy.incomePerSecond, energyDistribution.maxTimer, energyDistribution.energy.spentPerSecond);
        percentage.text = String.Format(EnergyText.PercentageCount, energyDistribution.energy.armyPercentage, energyDistribution.energy.energyPercentage);
    }

    void Update()
    {
        if(energyDistribution.timer <= 0) 
        {
            energyTextMesh.text = String.Format(EnergyText.EnergyCount, energyDistribution.energy.count, energyDistribution.maxTimer, income, energyDistribution.energy.incomePerSecond, energyDistribution.maxTimer, energyDistribution.energy.spentPerSecond);
            income = energyDistribution.energy.incomePerSecond + energyDistribution.energy.incomePerSecond * energyDistribution.energy.efficiency * (energyDistribution.energy.energyPercentage / 100f);
            percentage.text = String.Format(EnergyText.PercentageCount, energyDistribution.energy.armyPercentage, energyDistribution.energy.energyPercentage);
        }
    }

    public void ChangeValues()
    {
        percentage.text = String.Format(EnergyText.PercentageCount, energyDistribution.energy.armyPercentage, energyDistribution.energy.energyPercentage);
    }
}
