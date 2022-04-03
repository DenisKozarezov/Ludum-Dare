using UnityEngine;

namespace Core.Units.State
{
    public class WanderState : BaseState<UnitView>
    {
        private const float WanderRange = 3f;
        private Vector3 _currentPoint;
        private Vector3 _initPosition;

        public WanderState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {

        }

        private Vector2 GetRandomDestination()
        {
            Vector3 randomPoint = Random.insideUnitCircle * WanderRange;
            return _initPosition + new Vector3(randomPoint.x, randomPoint.y);
        }
        private bool ReachedDestination()
        {
            return (_currentPoint - Unit.transform.position).sqrMagnitude <= 0.1f;
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

            if (ReachedDestination())
            {
                _currentPoint = GetRandomDestination();
            }
            Vector2 direction = (_currentPoint - Unit.transform.position).normalized;
            Unit.Translate(direction);
        }
    }
}