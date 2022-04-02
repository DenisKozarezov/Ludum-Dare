using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyDistribution : MonoBehaviour
{
    public Energy energy = new Energy();
    public float timer = 1f;
    public float maxTimer = 1f;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI percentage;
    public Slider percentageSlider;
    public float targetEnergyCount = 1000f;

    private void Start() {
        percentageSlider.value = 50f;
        energyText.text = $"Energy: {energy.count}\nIncome({maxTimer}s): {energy.incomePerSecond + energy.incomePerSecond*energy.efficiency*(energy.energyPercentage/100f)}({energy.incomePerSecond})\nSpent({maxTimer}s): {energy.spentPerSecond}";
        percentage.text = $"Army: {energy.armyPercentage}%\nEnergy: {energy.energyPercentage}%";
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
            energyText.text = $"Energy: {energy.count}\nIncome({maxTimer}s): {energy.incomePerSecond + energy.incomePerSecond*energy.efficiency*(energy.energyPercentage/100f)}({energy.incomePerSecond})\nSpent({maxTimer}s): {energy.spentPerSecond}";
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
            energyText.text = $"Energy: {energy.count}\nIncome({maxTimer}s): {energy.incomePerSecond + energy.incomePerSecond*energy.efficiency*(energy.energyPercentage/100f)}({energy.incomePerSecond})\nSpent({maxTimer}s): {energy.spentPerSecond}";
        }
    }

    public void DistributeEnergy() 
    {
        energy.armyPercentage = percentageSlider.value;
        energy.energyPercentage = 100f - percentageSlider.value;
        percentage.text = $"Army: {energy.armyPercentage}%\nEnergy: {energy.energyPercentage}%";
    }
}
