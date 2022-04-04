using System.Linq;
using UnityEngine;
using Core.Units;

namespace Core.Abilities
{
    [CreateAssetMenu(menuName = "Configuration/Abilities/Effects/Create FireCircle")]
    public class FireCircleEffect : AbilityEffect
    {
        [Header("Characteristics")]
        [SerializeField]
        private LayerMask _layer;
        [SerializeField, Min(0)]
        private float _range;
        [SerializeField, Min(0)]
        private float _damage;

        public override void Execute()
        {
            var effect = CreateEffect();
            var sound = CreateSound();

            Collider2D[] result = new Collider2D[50];
            int hits = Physics2D.OverlapCircleNonAlloc(PlayerView.Instance.transform.position, _range, result, _layer.value);
            if (hits > 0)
            {
                result = result.Where(x => x != null).ToArray();
                foreach (var target in result)
                {
                    target.GetComponent<UnitView>().Hit(_damage, PlayerView.Instance);
                }
            }
        }
    }
}