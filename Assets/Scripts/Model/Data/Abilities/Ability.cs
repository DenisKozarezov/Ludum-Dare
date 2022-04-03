using System;
using UnityEngine;

namespace Core.Abilities
{
    [Serializable]
    public struct Ability
    {
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;
        [SerializeField]
        private byte _manacost;
        [SerializeField, Min(0)]
        private float _cooldown;
        [SerializeField]
        private UsageType _usageType;
        [SerializeField]
        private AbilityEffect _effect;
        [SerializeField]
        public Sprite _icon;

        public string DisplayName => _displayName;
        public string Description => _description;
        public byte Manacost => _manacost;
        public float Cooldown => _cooldown;
        public UsageType UsageType => _usageType;
        public AbilityEffect Effect => _effect;
        public Sprite Icon => _icon;
    }
}