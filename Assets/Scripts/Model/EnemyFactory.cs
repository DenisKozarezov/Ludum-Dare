using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Factory")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField]
        private UnitModel[] _units;

        public IReadOnlyCollection<UnitModel> Units => _units;
    }
}