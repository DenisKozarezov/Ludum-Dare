using System;
using UnityEngine;
using Core.UI;
using Core.Models;

namespace Core.Services
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private EnergyDistribution _energyDistribution;
        [SerializeField]
        private AbilitiesPanel _abilitiesPanel;
        [SerializeField]
        private PlayerModel _playerModel;

        private void Start()
        {
            foreach (var item in _playerModel.Abilities)
            {
                _abilitiesPanel.AddAbility(item.Value);
            }
        }

        public void SpendEnergy(float spawncost)
        {
            _energyDistribution.SpendEnergy(spawncost);
        }
    }
}