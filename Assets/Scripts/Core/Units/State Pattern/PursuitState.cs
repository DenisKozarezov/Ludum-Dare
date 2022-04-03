using UnityEngine;

namespace Core.Units.State
{
    public class PursuitState : BaseState<UnitView>
    {
        private const float SqrAttackRange = 0.25f;

        public PursuitState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {
          
        }

        private bool ReachedTarget(out Vector2 direction)
        {
            direction = Unit.Target.transform.position - Unit.transform.position;
            return direction.sqrMagnitude <= SqrAttackRange;
        }

        public override void Enter()
        {
            
        }
        public override void Exit()
        {
          
        }
        public override void Update()
        {
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