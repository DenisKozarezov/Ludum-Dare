using System;
using System.Collections;
using UnityEngine;
using Core.Models;
using Core.Units.State;

namespace Core.Units
{
    public abstract class UnitView : MonoBehaviour
    {
        [SerializeField]
        private UnitModel _unitData;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private RangeCheckingSystem _rangeCheckingSystem;   

        public UnitModel UnitData => _unitData;

        // ============ ANIMATION KEYS ============
        protected const string IDLE_KEY = "Idle";
        protected const string ATTACK_KEY = "Attack";
        protected const string DIED_KEY = "Death";
        // ======================================== 

        // ============== STATUSES ================
        public bool CanAttack { get; protected set; } = true;
        public bool Dead { get; private set; }
        public bool Invulnerable { get; set; }
        // ========================================

        // =============== COMBAT =================
        private bool _alreadyHit;
        public UnitView Target { get; private set; }
        // ========================================

        protected UnitStateMachine StateMachine { get; set; }

        public event Action<UnitRecievedDamageArgs> RecievedDamage;
        public event Action Died;

        protected abstract void Awake();
        protected abstract void Start();

        public virtual UnitState CreateState()
        {
            return new UnitState
            {
                MaxHealth = UnitData.Stats.MaxHealth,
                Health = UnitData.Stats.MaxHealth,                
            };
        }

        public void Disable()
        {
            CanAttack = false;
            Invulnerable = true;
            Target = null;
        }
        public void Hit(float damage, UnitView source = null)
        {
            if (Dead || Invulnerable) return;
            RecievedDamage?.Invoke(new UnitRecievedDamageArgs
            {
                Target = this,
                Source = source,
                Damage = damage
            });

            if (!_alreadyHit) StartCoroutine(HitCoroutine());
        }
        private IEnumerator HitCoroutine()
        {
            _alreadyHit = true;
            Color defaultColor = _spriteRenderer.material.color;
            for (int i = 0; i < 5; i++)
            {
                _spriteRenderer.material.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                _spriteRenderer.material.color = defaultColor;
            }
            _alreadyHit = false;
        }
        public void Kill()
        {
            if (Dead) return;
            StateMachine.SwitchState<DeadState>();
            Died?.Invoke();
        }
        public void Taunt(UnitView target)
        {
            if (target.Dead || Dead) return; 
            Target = target;
            StateMachine.SwitchState<PursuitState>();
        }
        public void Translate(Vector2 direction)
        {
            transform.Translate(direction * UnitData.Stats.MovementSpeed * Time.deltaTime);
            _spriteRenderer.flipX = direction.x <= 0;
        }
        public void Attack(UnitView target)
        {
            if (!CanAttack) return;
            target.Hit(UnitData.Stats.Damage, this);
            StartCoroutine(AttackCoroutine());
        }
        private IEnumerator AttackCoroutine()
        {
            CanAttack = false;
            yield return new WaitForSeconds(UnitData.Stats.AttackSpeed);
            CanAttack = true;
        }
    }
}