using System;
using System.Collections;
using UnityEngine;
using Core.Models;
using Core.Units.State;

namespace Core.Units
{
    [RequireComponent(typeof(Collider2D))]
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

        [Header("Sounds")]
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private UnitSounds _sounds;

        private UnitState _unitState;
        public UnitState UnitState
        {
            get
            {
                if (_unitState == null) _unitState = CreateState();
                return _unitState;
            }
        }
        public UnitModel UnitData
        {
            get
            {
#if UNITY_EDITOR
                if (_unitData == null)
                {
                    EditorExtensions.Log($"<b><color=yellow>'{gameObject.name}</color></b>' forgot to assign <b><color=yellow>unit model</color></b>!", EditorExtensions.LogType.Assert);
                }
#endif
                return _unitData;
            }
        }
    
        public Vector2 Size => GetComponent<Collider2D>().bounds.size;

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
        public event Action Collided;

        protected virtual void Awake()
        {
            StateMachine = new UnitStateMachine(this);
            _rangeCheckingSystem.SetRange(Constants.AggressionRadius);
            _rangeCheckingSystem.EnemyDetected += OnEnemyDetected;
            _rangeCheckingSystem.EnableChecking(true);
        }
        protected virtual void Start()
        {
            if (_unitData == null) return;
            StateMachine.SwitchState<WanderState>();
        }
        private void Update()
        {
            if (_unitData == null || Dead) return;

            StateMachine.CurrentState?.Update();
        }

        protected virtual UnitState CreateState()
        {
            return new UnitState
            {
                Health = _unitData.Stats.MaxHealth,
                Owner = UnitOwner.Neutral,
                UnitStats = new UnitStats
                {
                    MaxHealth = _unitData.Stats.MaxHealth,
                    Damage = _unitData.Stats.Damage,
                    AttackSpeed = _unitData.Stats.AttackSpeed,
                    Cost = _unitData.Stats.Cost,
                    HpRegeneration = _unitData.Stats.HpRegeneration,
                    MovementSpeed = _unitData.Stats.MovementSpeed,
                }
            };
        }

        private void OnEnemyDetected(UnitView target)
        {
            if (!Taunted)
            {
                _rangeCheckingSystem.EnableChecking(false);
                Taunt(target);
            }
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
            transform.Translate(direction * UnitState.UnitStats.MovementSpeed * Time.deltaTime);
            _spriteRenderer.flipX = direction.x <= 0;
        }
        public void Attack(UnitView target)
        {
            if (!CanAttack) return;
            target.Hit(UnitState.UnitStats.Damage, this);
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
            yield return new WaitForSeconds(UnitState.UnitStats.AttackSpeed);
            CanAttack = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke();
        }
    }
}