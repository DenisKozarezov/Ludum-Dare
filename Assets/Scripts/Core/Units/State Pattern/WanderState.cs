using UnityEngine;

namespace Core.Units.State
{
    public class WanderState : BaseState<UnitView>
    {
        public WanderState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        //private Vector2 GetRandomPoint()
        //{
        //    Vector3 randomPoint = Random.insideUnitCircle * _wanderRange;
        //    return _initPosition + new Vector3(randomPoint.x, 0, randomPoint.y);
        //}

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