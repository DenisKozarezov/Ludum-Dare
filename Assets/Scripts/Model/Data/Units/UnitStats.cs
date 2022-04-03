using System;
using UnityEngine;

namespace Core.Units
{
    [Serializable]
    public struct UnitStats
    {
        [Min(0)]
        public float MaxHealth;
        [Min(0)]
        public float Damage;
        [Range(0f, 5f)]
        public float MovementSpeed;
        [Range(0f, 10f)]
        public float AttackSpeed;
        [Min(0)]
        public float Cost;
        [Min(0)]
        public float HpRegeneration;

        public static UnitStats operator +(UnitStats first, UnitStats second)
        {
            return new UnitStats
            {
                MaxHealth = first.MaxHealth + second.MaxHealth,
                Damage = first.Damage + second.Damage,
                MovementSpeed = first.MovementSpeed + second.MovementSpeed,
                AttackSpeed = first.AttackSpeed + second.AttackSpeed,
                Cost = first.Cost + second.Cost,
                HpRegeneration = first.HpRegeneration + second.HpRegeneration,
            };
        }
        public static UnitStats operator *(UnitStats first, float value)
        {
            return new UnitStats
            {
                MaxHealth = first.MaxHealth * value,
                Damage = first.Damage * value,
                MovementSpeed = first.MovementSpeed * value,
                AttackSpeed = first.AttackSpeed * value,
                Cost = first.Cost * value,
                HpRegeneration = first.HpRegeneration * value,
            };
        }
    }
}