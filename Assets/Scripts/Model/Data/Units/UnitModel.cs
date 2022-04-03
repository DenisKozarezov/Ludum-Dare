using System;
using UnityEngine;
using Core.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Unit Model")]
    public class UnitModel : ScriptableObject, IEquatable<UnitModel>
    {
        [Header("Common Characteristics")]
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;
        [SerializeField]
        private UnitType _unitType;
        [SerializeField]
        private Specialization _specialization;
        [SerializeField]
        private RaceType _raceType;
        [SerializeField]
        private UnitOwner _owner;

        [Space, SerializeField]
        private string _prefabPath;

        [Header("Combat Characteristics")]
        [SerializeField]
        private UnitStats _stats;

        public uint ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public UnitType UnitType => _unitType;
        public Specialization Specialization => _specialization;
        public RaceType RaceType => _raceType;
        public UnitOwner Owner => _owner;
        public string PrefabPath => _prefabPath;
        public UnitStats Stats => _stats;       

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(_displayName))
            {
                _displayName = name;
            }
        }

        public bool Equals(UnitModel other)
        {
            return _id == other._id;
        }
    }
}