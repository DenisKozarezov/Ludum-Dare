namespace Core.Units.State
{
    public abstract class BaseState<T>
    {
        public readonly T Unit;
        protected readonly IStateMachine<T> StateMachine;

        public BaseState(T unit, IStateMachine<T> stateMachine)
        {
            Unit = unit;
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}