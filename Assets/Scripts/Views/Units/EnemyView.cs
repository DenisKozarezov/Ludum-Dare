using Core.Services;
using Core.Units.State;

namespace Core.Units
{
    public class EnemyView : UnitView, IEnemy
    {
        public bool Taunted { get; private set; }

        protected override void Awake()
        {
            StateMachine = new UnitStateMachine(this);
        }
        protected override void Start()
        {
            if (UnitData == null) return;
            UnitsManager.RegisterUnit(this);

            Taunt(PlayerView.Instance);
        }
        private void Update()
        {
            if (Dead) return;

            StateMachine.CurrentState.Update();
        }
    }
}