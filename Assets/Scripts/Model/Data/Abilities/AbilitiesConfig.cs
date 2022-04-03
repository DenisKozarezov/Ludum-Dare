using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Core.Abilities;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Create Config")]
    public class AbilitiesConfig : ScriptableObject
    {
        [SerializeField]
        private SerializableDictionaryBase<uint, Ability> _abilities;

        public Dictionary<uint, Ability> Abilities => _abilities.Clone();
    }
}