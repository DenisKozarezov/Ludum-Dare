namespace Core.Units.State
{
    public class AttackState : BaseState<UnitView>
    {
        private const float SqrAttackRange = 0.25f;

        public AttackState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        private bool ReachedTarget()
        {
            return (Unit.Target.transform.position - Unit.transform.position).sqrMagnitude <= SqrAttackRange;
        }

        public override void Enter()
        {
   
        }

        public override void Exit()
        {
   
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