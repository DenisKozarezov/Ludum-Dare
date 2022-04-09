using System.Collections.Generic;
using UnityEngine;

namespace Core.Navigation
{
    public interface IPathfinder
    {
        bool PathValid { get; }
        bool PathComplete { get; }
        bool IsStopped { get; }
        LinkedList<Node> CalculatePath(Vector2 startPosition, Vector2 endPosition);
        void SetDestination(Vector2 destination);
        void Move();
        void Stop();
    }
}