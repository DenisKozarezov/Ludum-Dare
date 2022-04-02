using System.Collections.Generic;
using UnityEngine;
using Core.Models;

namespace Core.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Create Enemy Factory")]
    public class EnemyFactory : UnitFactory<EnemyModel>
    {
        [SerializeField]
        private List<EnemyModel> _enemies;

        protected override EnemyModel GetConfig<Unit>()
        {
            return _enemies.Find(x => x is Unit);
        }

        protected override EnemyModel GetRandomConfig()
        {
            return _enemies[Random.Range(0, _enemies.Count - 1)];
        }
    }
}