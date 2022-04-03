using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    public EnergyDistribution energyDistribution;
    public TextMeshProUGUI energyTextMesh;
    public TextMeshProUGUI spentTextMesh;
    public TextMeshProUGUI percentage;
    
    private float income = 0;

    void Start()
    {
        if (energyDistribution.OnValueChange == null) energyDistribution.OnValueChange = new UnityEvent();
        UpdateValues();
        energyDistribution.OnValueChange.AddListener(UpdateValues);
    }

    public void UpdateValues()
    {
        income = energyDistribution.energy.incomePerSecond + energyDistribution.energy.efficiency * (energyDistribution.energy.spentPerSecond*(energyDistribution.energy.energyPercentage / 100f));
        energyTextMesh.text = String.Format(EnergyText.EnergyCount, energyDistribution.energy.count, energyDistribution.maxTimer, income, energyDistribution.energy.incomePerSecond, energyDistribution.maxTimer, energyDistribution.energy.spentPerSecond);
        percentage.text = String.Format(EnergyText.PercentageCount, energyDistribution.energy.armyPercentage, energyDistribution.energy.energyPercentage);
        spentTextMesh.text = String.Format(EnergyText.SpentPercent, energyDistribution.spentSlider.value);
    }
}
