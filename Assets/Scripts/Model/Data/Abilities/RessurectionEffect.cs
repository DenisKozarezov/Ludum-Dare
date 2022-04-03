using System.Linq;
using UnityEngine;
using Core.Units;

namespace Core.Abilities
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Effects/Create Ressurection Effect")]
    public class RessurectionEffect : AbilityEffect
    {
        [Header("Characteristics")]
        [SerializeField]
        private LayerMask _layer;
        [SerializeField, Min(0)]
        private float _range;
        [SerializeField]
        private byte _unitsCount;

        public override void Execute()
        {
            Collider2D[] result = new Collider2D[_unitsCount];
            int hits = Physics2D.OverlapCircleNonAlloc(PlayerView.Instance.transform.position, _range, result, _layer.value);
            if (hits > 0)
            {
                result = result.Where(x => x != null).ToArray();
                foreach (var target in result)
                {
                    // Ressurect
                }
            }
        }
    }
}