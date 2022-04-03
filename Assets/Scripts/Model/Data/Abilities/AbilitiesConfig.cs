using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Core.Abilities;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Create Config")]
    public class AbilitiesConfig : ScriptableObject
    {
        public SerializableDictionaryBase<uint, Ability> Abilities;
    }
}