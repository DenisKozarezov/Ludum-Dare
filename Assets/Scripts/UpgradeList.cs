using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.Units;

public class UpgradeList : MonoBehaviour
{
    public List<Upgrade> upgrades = new List<Upgrade>();

    System.Random rnd = new System.Random();

    public GameObject upgradePanel;
    
    public EnergyDistribution energyDistribution;
    public UnitsManager unitsManager;
    public WaveManager waveManager;

    private void Start()
    {
        upgrades.Add(new Upgrade(energyDistribution, "energy", "", "add", "incomePerPercent", 0.5f));
        upgrades.Add(new Upgrade(unitsManager, "units", "", new UnitUpgradeArgs {
            AddMaxHealth = 1f,
            AddDamage = 1f,
            AddMovementSpeed = 100f,
            AddAttackSpeed = 1f,
            AddHpRegeneration = 1f
        }));
        upgrades.Add(new Upgrade(waveManager, "unitSpawner", "", 2, 2));
        // upgrades.Add(new Upgrade(unitsManager, "4", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(energyDistribution, "5", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(energyDistribution, "6", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(unitsManager, "7", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(unitsManager, "8", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(energyDistribution, "9", "", "add", "incomePerPercent", 0.5f));
        // upgrades.Add(new Upgrade(energyDistribution, "10", "", "add", "incomePerPercent", 0.5f));
    }

    public List<Upgrade> GetRandomUpgrades()
    {
        return upgrades.OrderBy(x => rnd.Next()).Take(3).ToList();
    }

    public void SendUpgrades()
    {
        upgradePanel.SetActive(true);
        List<Upgrade> upgradeList = GetRandomUpgrades();
        upgradePanel.transform.Find("Upgrade1").GetComponent<ButtonUI>().getUpgradeText(upgradeList[0]);
        upgradePanel.transform.Find("Upgrade1").GetComponent<ButtonListener>().getUpgrade(upgradeList[0]);
        upgradePanel.transform.Find("Upgrade2").GetComponent<ButtonUI>().getUpgradeText(upgradeList[1]);
        upgradePanel.transform.Find("Upgrade2").GetComponent<ButtonListener>().getUpgrade(upgradeList[1]);
        upgradePanel.transform.Find("Upgrade3").GetComponent<ButtonUI>().getUpgradeText(upgradeList[2]);
        upgradePanel.transform.Find("Upgrade3").GetComponent<ButtonListener>().getUpgrade(upgradeList[2]);
    }

    public void DisableButtons()
    {
        upgradePanel.SetActive(false);
    }
}
