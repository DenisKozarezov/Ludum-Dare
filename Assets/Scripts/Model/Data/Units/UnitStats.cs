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
        [Min(0)]
        public float Strength;
        [Min(0)]
        public float Agility;
        [Min(0)]
        public float Intelligence;
        [Range(0f, 5f)]
        public float MovementSpeed;
        [Range(0f, 10f)]
        public float AttackSpeed;
        [Min(0)]
        public float HpRegeneration;

        public static UnitStats operator +(UnitStats first, UnitStats second)
        {
            return new UnitStats
            {
                MaxHealth = first.MaxHealth + second.MaxHealth,
                Damage = first.Damage + second.Damage,
                Strength = first.Strength + second.Strength,
                Agility = first.Agility + second.Agility,
                Intelligence = first.Intelligence + second.Intelligence,
                MovementSpeed = first.MovementSpeed + second.MovementSpeed,
                AttackSpeed = first.AttackSpeed + second.AttackSpeed,
                HpRegeneration = first.HpRegeneration + second.HpRegeneration,
            };
        }
        public static UnitStats operator *(UnitStats first, float value)
        {
            return new UnitStats
            {
                MaxHealth = first.MaxHealth * value,
                Damage = first.Damage * value,
                Strength = first.Strength * value,
                Agility = first.Agility * value,
                Intelligence = first.Intelligence * value,
                MovementSpeed = first.MovementSpeed * value,
                AttackSpeed = first.AttackSpeed * value,
                HpRegeneration = first.HpRegeneration * value,
            };
        }
    }
}