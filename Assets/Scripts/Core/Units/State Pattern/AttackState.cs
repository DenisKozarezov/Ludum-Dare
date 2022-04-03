using Core.Services;

namespace Core.Units.State
{
    public class AttackState : BaseState<UnitView>
    {
        public AttackState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        private void OnTargetDied()
        {
            var nearestEnemy = UnitsManager.GetNearestEnemy(Unit);
            if (nearestEnemy != null) Unit.Taunt(nearestEnemy);
            else Unit.Wander();
        }
        private bool ReachedTarget()
        {
            return (Unit.Target.transform.position - Unit.transform.position).sqrMagnitude <= Constants.AttackRadius * Constants.AttackRadius;
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
            if (ReachedTarget())
            {
                if (Unit.CanAttack) Unit.Attack(Unit.Target);
            }
            else
            {
                StateMachine.SwitchState<PursuitState>();
            }
        }
    }
}