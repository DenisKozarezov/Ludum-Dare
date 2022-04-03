using Core.Services;
using Core.Units.State;

namespace Core.Units
{
    public class PlayerView : UnitView
    {
        private static PlayerView _instance;
        public static PlayerView Instance => _instance;

        protected override void Awake()
        {
            if (_instance == null) _instance = this;
            StateMachine = new UnitStateMachine(this);
        }
        protected override void Start()
        {
            if (UnitData == null) return;
            StateMachine.SwitchState<WanderState>();
        }
        private void Update()
        {
            if (Dead) return;

            StateMachine.CurrentState.Update();
        }
    }
}