using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyDistribution : MonoBehaviour
{
    public Energy energy = new Energy();
    public Slider percentageSlider;
    public Slider spentSlider;

    public float timer = 1f;
    public float maxTimer = 1f;
    public float targetEnergyCount = 1000f;

    public UnityEvent OnValueChange;
    public UnityEvent Win;

    private void Start() 
    {
        percentageSlider.value = 50f;
        spentSlider.value = 50f;
        energy.spentPerSecond = energy.count*(spentSlider.value/100);
        OnValueChange?.Invoke();
    }

    private void Update() 
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            energy.spentPerSecond = energy.count*(spentSlider.value/100);
            if (energy.count >= energy.spentPerSecond) 
            {
                energy.count -= energy.spentPerSecond;
            }
            energy.count += energy.incomePerSecond + energy.efficiency*(energy.spentPerSecond*(energy.energyPercentage/100));
            timer = maxTimer;
            OnValueChange?.Invoke();
            if (energy.count >= targetEnergyCount) 
            {
                Win?.Invoke();
            }
        }
    }

    public void DistributeEnergy() 
    {
        energy.armyPercentage = percentageSlider.value;
        energy.energyPercentage = 100f - percentageSlider.value;
        OnValueChange?.Invoke();
    }
}