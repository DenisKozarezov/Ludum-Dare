using UnityEngine;
using Core.Services;

namespace Core.Units.State
{
    public class PursuitState : BaseState<UnitView>
    {
        public PursuitState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {
        
        }

        private void OnTargetDied()
        {
            var nearestEnemy = UnitsManager.GetNearestEnemy(Unit);
            if (nearestEnemy != null) Unit.Taunt(nearestEnemy);
            else Unit.Wander();
        }

        private bool ReachedTarget(out Vector2 direction)
        {       
            direction = Unit.Target.transform.position - Unit.transform.position;
            return direction.sqrMagnitude <= Constants.AttackRadius * Constants.AttackRadius;
        }

        public override void Enter()
        {
            Unit.Target.Died += OnTargetDied;
        }
        public override void Exit()
        {
            Unit.Target.Died -= OnTargetDied;
        }
        public override void Update()
        {
            if (Unit.Dead) return;

            if (!ReachedTarget(out Vector2 direction))
            {
                Unit.Translate(direction.normalized);
            }
            else
            {
                StateMachine.SwitchState<AttackState>();
            }
        }
    }
}