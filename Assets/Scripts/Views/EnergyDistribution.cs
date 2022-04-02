using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyDistribution : MonoBehaviour
{
    public Energy energy = new Energy();
    public float timer = 1f;
    public float maxTimer = 1f;
    public Slider percentageSlider;
    public float targetEnergyCount = 1000f;

    private void Start() {
        percentageSlider.value = 50f;        
    }

    private void Update() {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            if (energy.count >= energy.spentPerSecond) 
            {
                energy.count -= energy.spentPerSecond;
            }
            energy.count += energy.incomePerSecond + energy.incomePerSecond*energy.efficiency*(energy.energyPercentage/100f);
            timer = maxTimer;
            if (energy.count >= targetEnergyCount) 
            {
                Debug.Log("Win!");
            }
        }
    }

    public void Spent(float increment)
    {
        if (energy.spentPerSecond > 0 || increment > 0) {
            energy.spentPerSecond += increment;
        }
    }

    public void DistributeEnergy() 
    {
        energy.armyPercentage = percentageSlider.value;
        energy.energyPercentage = 100f - percentageSlider.value;
    }
}
