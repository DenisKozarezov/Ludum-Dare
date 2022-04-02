using Core.Models;
using UnityEngine;

namespace Core.Units
{
    public abstract class UnitFactory<T> : ScriptableObject where T : UnitModel
    {
        protected abstract T GetConfig<Unit>() where Unit : T;
        protected abstract T GetRandomConfig();

        public UnitView GetUnit<Unit>() where Unit : T
        {
            var config = GetConfig<T>();
            var unit = Instantiate(Resources.Load<GameObject>(config.PrefabPath));
            return unit.GetComponent<UnitView>();
        }
        public UnitView GetRandomUnit()
        {
            var config = GetRandomConfig();
            var unit = Instantiate(Resources.Load<GameObject>(config.PrefabPath));
            return unit.GetComponent<UnitView>();
        }
    }
}