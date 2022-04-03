using System.Linq;
using UnityEngine;

namespace Core.Units.State
{
    public class WanderState : BaseState<UnitView>
    {
        private Vector2 _currentPoint;
        private Vector2 _initPosition;

        public WanderState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        private Vector2 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * Constants.WanderRange;
        }
        private bool ReachedDestination(out Vector2 direction)
        {
            direction = _currentPoint - (Vector2)Unit.transform.position;
            return direction.sqrMagnitude <= 0.1f;
        }

        public override void Enter()
        {
            _initPosition = Unit.transform.position;
            _currentPoint = GetRandomDestination();
        }
        public override void Exit()
        {
           
        }
        public override void Update()
        {
#if UNITY_EDITOR
            Debug.DrawLine(Unit.transform.position, _currentPoint, Color.yellow);
#endif

            if (ReachedDestination(out Vector2 direction))
            {
                _currentPoint = GetRandomDestination();
            }
            Unit.Translate(direction.normalized);
        }
    }
}