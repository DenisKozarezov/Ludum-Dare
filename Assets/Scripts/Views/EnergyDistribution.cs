using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class EnergyDistribution : MonoBehaviour
{
    public object this[string propertyName]
    {
        get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
        set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
    }

    private bool _enabled;

    public Slider percentageSlider;
    
    public int baseDivision = 100;
    public float energyLimit = 1000f;

    public float count = 100f;
    public float armyPercentage = 50f;
    public float energyPercentage = 50f;
    public float coeff = 1f;
    public float timer = 1f;
    public float maxTimer = 1f;
    public float targetEnergyCount = 1000f;

    public UnityEvent OnValueChange;
    public UnityEvent Win;

    public float incomePerPercent { get; set; }

    void Start() 
    {
        incomePerPercent = 1f;
        percentageSlider.value = 50f;
    }

    void Update() 
    {
        if (!_enabled) return;

        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            coeff = (int)count / baseDivision;
            count = Math.Min(count+coeff*incomePerPercent*energyPercentage, energyLimit);
            timer = maxTimer;
            OnValueChange?.Invoke();
            if (count >= targetEnergyCount) 
            {
                Win?.Invoke();
            }
        }
    }

    public void SpendEnergy(float spawncost)
    {
        count -= spawncost;
    }

    public void DistributeEnergy() 
    {
        armyPercentage = percentageSlider.value;
        energyPercentage = 100f - percentageSlider.value;
        OnValueChange?.Invoke();
    }

    public void Enable(bool isEnabled)
    {
        _enabled = isEnabled;
    }
}