using System;
using System.Collections;
using UnityEngine;
using Core.Models;
using Core.Units.State;

namespace Core.Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField]
        private UnitModel _unitData;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Rigidbody2D _rigibody;
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
        public bool Taunted { get; private set; }
        public UnitView Target { get; private set; }
        // ========================================

        protected UnitStateMachine StateMachine { get; set; }

        public event Action<UnitRecievedDamageArgs> RecievedDamage;
        public event Action Died;

        protected virtual void Awake()
        {
            StateMachine = new UnitStateMachine(this);
            _rangeCheckingSystem.SetRange(Constants.AggressionRadius);
            _rangeCheckingSystem.EnemyDetected += OnEnemyDetected;
            _rangeCheckingSystem.EnableChecking(true);
        }
        protected virtual void Start()
        {
            if (UnitData == null) return;
            StateMachine.SwitchState<WanderState>();
        }
        private void Update()
        {
            if (Dead) return;

            StateMachine.CurrentState?.Update();
        }

        private void OnEnemyDetected(UnitView target)
        {
            if (!Taunted)
            {
                _rangeCheckingSystem.EnableChecking(false);
                Taunt(target);
            }
        }

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
            StopAllCoroutines();

            CanAttack = false;
            Invulnerable = true;

            _rigibody.constraints = RigidbodyConstraints2D.FreezeAll;
            foreach (var collider in GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
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
            _spriteRenderer.material.color = Color.red;
            yield return new WaitForSeconds(1f);
            _spriteRenderer.material.color = defaultColor;
            _alreadyHit = false;
        }
        public void Kill()
        {
            if (Dead) return;
            Disable();
            StateMachine.SwitchState<DeadState>();
            Died?.Invoke();

            Destroy(gameObject, Constants.DeathTimer);
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
        public void Wander()
        {
            _rangeCheckingSystem.EnableChecking(true);
            StateMachine.SwitchState<WanderState>();
        }
        private IEnumerator AttackCoroutine()
        {
            CanAttack = false;
            yield return new WaitForSeconds(UnitData.Stats.AttackSpeed);
            CanAttack = true;
        }
    }
}