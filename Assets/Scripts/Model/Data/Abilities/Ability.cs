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
        [SerializeField]
        private UsageType _usageType;
        [SerializeField]
        public Sprite _icon;

        public string DisplayName => _displayName;
        public string Description => _description;
        public byte Manacost => _manacost;
        public UsageType UsageType => _usageType;
        public Sprite Icon => _icon;
    }
}