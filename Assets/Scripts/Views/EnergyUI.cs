using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    public EnergyDistribution energyDistribution;
    public TextMeshProUGUI energyTextMesh;
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
        income = energyDistribution.coeff * energyDistribution.incomePerPercent * energyDistribution.energyPercentage;
        energyTextMesh.text = String.Format(EnergyText.EnergyCount, energyDistribution.count, energyDistribution.maxTimer, income, energyDistribution.incomePerPercent);
        percentage.text = String.Format(EnergyText.PercentageCount, energyDistribution.armyPercentage, energyDistribution.energyPercentage);
    }
}
