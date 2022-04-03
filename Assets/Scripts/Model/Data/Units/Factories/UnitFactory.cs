using Core.Units;
using UnityEngine;

namespace Core.Models
{
    public abstract class UnitFactory<T> : ScriptableObject where T : UnitModel
    {
        protected abstract T GetConfig<Unit>() where Unit : T;
        protected abstract T GetRandomConfig();

        public UnitView GetUnit<Unit>() where Unit : T
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