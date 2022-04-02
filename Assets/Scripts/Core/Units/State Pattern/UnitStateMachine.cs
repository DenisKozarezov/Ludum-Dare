using System.Collections.Generic;

namespace Core.Units.State
{
    public class UnitStateMachine : BaseStateMachine<UnitView>, IStateMachine<UnitView>
    {
        public UnitStateMachine(UnitView unit)
        {
            States = new List<BaseState<UnitView>>()
            {
                new AttackState(unit, this),
                new PursuitState(unit, this),
                new WanderState(unit, this),
                new DeadState(unit, this),
            };
        }
    }
}