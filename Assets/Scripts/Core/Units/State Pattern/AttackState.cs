namespace Core.Units.State
{
    public class AttackState : BaseState<UnitView>
    {
        private const float SqrAttackRange = 0.25f;

        public AttackState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        private void OnTargetDied()
        {
            StateMachine.SwitchState<WanderState>();
        }
        private bool ReachedTarget()
        {
            return (Unit.Target.transform.position - Unit.transform.position).sqrMagnitude <= SqrAttackRange;
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