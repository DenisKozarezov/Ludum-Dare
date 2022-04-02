namespace Core.Units.State
{
    public class DeadState : BaseState<UnitView>
    {
        public DeadState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }
        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}