using UnityEngine;
using TMPro;

public class EnergyDistribution : MonoBehaviour
{
    public Energy energy = new Energy();
    public float newPercentage;
    public float newSpentPerSecond;
    public float timer = 1f;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI procentage;

    private void Start() {
        DistributeEnergy();
        energyText.text = $"Energy: {energy.count}\nIncome({timer}s): {energy.incomePerSecond}\nSpent({timer}s): {energy.spentPerSecond}";
    }

    private void Update() {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            energy.count += energy.incomePerSecond - energy.spentPerSecond;
            timer = 1f;
            energyText.text = $"Energy: {energy.count}\nIncome({timer}s): {energy.incomePerSecond}\nSpent({timer}s): {energy.spentPerSecond}";
        }
    }

    void DistributeEnergy() 
    {
        energy.armyPercentage = newPercentage;
        energy.energyPercentage = 100f - newPercentage;
        procentage.text = $"Army: {energy.armyPercentage}%\nEnergy: {energy.energyPercentage}%";
    }
}
