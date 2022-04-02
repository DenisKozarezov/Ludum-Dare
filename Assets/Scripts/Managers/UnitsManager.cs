using System.Collections.Generic;
using UnityEngine;
using Core.Units;
using Core.Models;

namespace Core.Services
{
    public class UnitsManager : MonoBehaviour
    {
        private static UnitsManager _instance;
        private Dictionary<UnitView, UnitState> Units = new Dictionary<UnitView, UnitState>();
        private ushort _enemiesCount;

        private void Awake()
        {
            if (_instance == null) _instance = this;
        }

        private void OnDamageRecieve(UnitRecievedDamageArgs args)
        {
            UnitView unit = args.Target;
            if (!Units.ContainsKey(unit)) return;

            if (Units[unit].Health - args.Damage > 0f)
            {
                Units[unit].Health -= args.Damage;
            }
            else
            {
                Units[unit].Health = 0f;

            }
        }
        private void OnUnitHealed(UnitView unit, float value)
        {
            if (!Units.ContainsKey(unit) || Units[unit].Health == Units[unit].MaxHealth) return;

            if (Units[unit].Health + value <= Units[unit].MaxHealth)
            {
                Units[unit].Health += value;
            }
            else
            {
                Units[unit].Health = Units[unit].MaxHealth;
            }
        }

        private void RemoveUnit(UnitView unit)
        {
            Units.Remove(unit);
        }
        private void Kill(UnitView unit)
        {
            unit.Kill();
            RemoveUnit(unit);

            if (unit is IEnemy) _enemiesCount--;

#if UNITY_EDITOR
            Debug.Log($"Unit <b><color=yellow>{unit.UnitData.DisplayName}</color></b> was <b><color=red>killed</color></b>. Units remained: <b><color=green>{Units.Count}</color></b>.");
#endif
        }

        public static void RegisterUnit(UnitView unit)
        {
            if (_instance.Units.ContainsKey(unit)) return;
            if (unit is IEnemy) _instance._enemiesCount++;
            _instance.Units.Add(unit, unit.CreateState());
        }
    }
}