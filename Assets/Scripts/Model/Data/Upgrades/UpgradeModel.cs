using UnityEngine;

namespace Core.Models
{
    public class UpgradeModel : ScriptableObject
    {
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField]
        private string _description;
        [Range(0, 5)]
        private byte _updradeTier;

        public uint ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public byte UpgradeTier => _updradeTier;
    }
}