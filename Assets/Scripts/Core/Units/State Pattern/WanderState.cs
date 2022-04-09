using Core.Navigation;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Units.State
{
    public class WanderState : BaseState<UnitView>
    {
        private readonly IPathfinder Pathfinder;
        private Vector2 _initPosition;

        private IReadOnlyList<Vector2> _path;

        public WanderState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {
            Pathfinder = Unit.Pathfinder;
        }

        //private void OnCollided()
        //{
        //    CalculateRandomDestination();
        //}

        private Vector2 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * Constants.WanderRange;
        }

        public override void Enter()
        {
            _initPosition = Unit.transform.position;
            Pathfinder.SetDestination(GetRandomDestination());
            //Unit.Collided += OnCollided;
        }
        public override void Exit()
        {
            //Unit.Collided -= OnCollided;
            Pathfinder.Stop();
        }
        public override void Update()
        {
            if (Unit.Dead) return;

            if (!Pathfinder.PathValid || Pathfinder.PathComplete)
            {              
                Pathfinder.SetDestination(GetRandomDestination());
            }

            Pathfinder.Move();
        }
    }
}