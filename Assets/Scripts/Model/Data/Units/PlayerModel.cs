using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player")]
    public class PlayerModel : UnitModel
    {
        [SerializeField]
        private AbilitiesConfig _abilities;

        public IReadOnlyDictionary<uint, Abilities.Ability> Abilities => _abilities.Abilities;
    }
}