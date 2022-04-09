using UnityEngine;

namespace Core.Navigation
{
    public interface IWalkable
    {
        Vector2 Size { get; }
        IPathfinder Pathfinder { get; }
        void Translate(Vector2 direction);
    }
}