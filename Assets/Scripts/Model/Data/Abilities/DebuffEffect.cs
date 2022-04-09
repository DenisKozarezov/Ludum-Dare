using System.Linq;
using UnityEngine;
using Core.Units;

namespace Core.Abilities
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Effects/Create Debuff Effect")]
    public class DebuffEffect : AbilityEffect
    {
        [Header("Characteristics")]
        [SerializeField]
        private LayerMask _layer;
        [SerializeField, Min(0)]
        private float _range;

        public override void Execute(UnitView source, UnitView target = null)
        {
            Collider2D[] result = new Collider2D[10];
            int hits = Physics2D.OverlapCircleNonAlloc(source.transform.position, _range, result, _layer.value);
            if (hits > 0)
            {
                result = result.Where(x => x != null).ToArray();
                foreach (var unit in result)
                {
                    // Debuff
                }
            }
        }
    }
}