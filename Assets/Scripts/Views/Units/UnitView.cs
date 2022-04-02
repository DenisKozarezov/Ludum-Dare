using System;
using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public abstract class UnitView : MonoBehaviour
    {
        [SerializeField]
        private UnitModel _unitModel;
        [SerializeField]
        private Animator _animator;

        public bool Dead;

        public UnitModel UnitModel => _unitModel;

        public event Action<UnitRecievedDamageArgs> RecievedDamage;
        public event Action<UnitView> Died;

        protected abstract void Start();
        protected abstract void Update();

        public void Hit(ushort damage, UnitView source = null)
        {
            RecievedDamage?.Invoke(new UnitRecievedDamageArgs
            {
                Target = this,
                Source = source,
                Damage = damage
            });
        }
        public void Kill(UnitView source = null)
        {
            Died?.Invoke(source);
        }
    }
}