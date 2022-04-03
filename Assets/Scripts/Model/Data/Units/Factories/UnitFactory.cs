using System.Collections.Generic;
using UnityEngine;
using Core.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Unit Factory")]
    public class UnitFactory : ScriptableObject
    {
        [SerializeField]
        private List<UnitModel> _units;

        protected UnitModel GetConfig<T>() where T : UnitModel
        {
            return _units.Find(x => x is T);
        }
        protected UnitModel GetRandomConfig()
        {
            return _units[Random.Range(0, _units.Count)];
        }

        public UnitView GetUnit<T>() where T : UnitModel
        {
            var config = GetConfig<T>();
            return Services.UnitsManager.InstantiateUnit(config.ID);
        }
        public UnitView GetRandomUnit()
        {
            var config = GetRandomConfig();
            return Services.UnitsManager.InstantiateUnit(config.ID);
        }
    }
}