using UnityEngine;

namespace Core.Units.State
{
    public class WanderState : BaseState<UnitView>
    {
        private Vector2 _currentPoint;
        private Vector2 _initPosition;
        private readonly float _reachDistance;

        public WanderState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {
            _reachDistance = unit.Size.magnitude;
        }

        private void OnCollided()
        {
            _currentPoint = GetRandomDestination();
        }

        private Vector2 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * Constants.WanderRange;
        }
        private bool ReachedDestination(out Vector2 direction)
        {
            direction = _currentPoint - (Vector2)Unit.transform.position;
            return direction.sqrMagnitude <= _reachDistance;
        }

        public override void Enter()
        {
            _initPosition = Unit.transform.position;
            _currentPoint = GetRandomDestination();
            Unit.Collided += OnCollided;
        }
        public override void Exit()
        {
            Unit.Collided -= OnCollided;
        }
        public override void Update()
        {
            if (Unit.Dead) return;

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