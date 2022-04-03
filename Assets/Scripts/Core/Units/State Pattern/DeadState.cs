namespace Core.Units.State
{
    public class DeadState : BaseState<UnitView>
    {
        public DeadState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        public override void Enter()
        {
            
        }
        public override void Exit()
        {
         
        }
        public override void Update()
        {
          
        }
    }
}