using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Units;
using Core.Models;

namespace Core.Services
{
    public class UnitsManager : MonoBehaviour
    {
        private static UnitsManager _instance;

        [SerializeField]
        private Transform _unitsRoot;

        private Lazy<UnitModel[]> AllUnits;
        private Dictionary<UnitView, UnitState> Units = new Dictionary<UnitView, UnitState>();

        private void Awake()
        {
            if (_instance == null) _instance = this;
            AllUnits = CreateLazyArray<UnitModel>("");
        }

        private Lazy<T[]> CreateLazyArray<T>(string path) where T : UnityEngine.Object
        {
            return new Lazy<T[]>(() => Resources.LoadAll<T>(path));
        }

        public static UnitView InstantiateUnit(uint id)
        {
            UnitModel data = _instance.AllUnits.Value.FirstOrDefault(x => x.ID == id);

            if (data == null)
            {
#if UNITY_EDITOR
                EditorExtensions.Log($"Unable to instantiate: there is no unit with such ID = {id}.", EditorExtensions.LogType.Warning);
#endif
                return null;
            }

            var prefab = Resources.Load<GameObject>(data.PrefabPath);
            if (prefab == null)
            {
#if UNITY_EDITOR
                EditorExtensions.Log("Unable to instantiate: can't load prefab from <color=yellow>Resources</color> folder.", EditorExtensions.LogType.Warning);
#endif
                return null;
            }

            var unit = GameObject.Instantiate(prefab, _instance._unitsRoot);
            var view = unit.GetComponentInChildren<UnitView>();
            _instance.OnUnitCreated(view);
            return view;
        }

        private void OnUnitCreated(UnitView unit)
        {
            unit.RecievedDamage += OnRecievedDamage;
            unit.Died += () =>
            {
                unit.RecievedDamage -= OnRecievedDamage;
            };
            RegisterUnit(unit);
        }
        private void OnRecievedDamage(UnitRecievedDamageArgs args)
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
                Kill(unit);
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

#if UNITY_EDITOR
            Debug.Log($"Unit <b><color=yellow>{unit.UnitData.DisplayName}</color></b> was <b><color=red>killed</color></b>. Units remained: <b><color=green>{Units.Count}</color></b>.");
#endif
        }

        private void RegisterUnit(UnitView unit)
        {
            if (Units.ContainsKey(unit)) return;
            Units.Add(unit, unit.CreateState());
        }
    }
}