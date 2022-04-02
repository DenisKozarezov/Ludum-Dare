using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Player Factory")]
    public class PlayerFactory : UnitFactory<UnitModel>
    {
        [SerializeField]
        private List<UnitModel> _units;

        protected override UnitModel GetConfig<T>()
        {
            return _units.Find(x => x is T);
        }

        protected override UnitModel GetRandomConfig()
        {
            return _units[Random.Range(0, _units.Count - 1)];
        }
    }
}