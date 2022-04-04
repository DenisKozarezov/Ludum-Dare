using UnityEngine;

namespace Core.Navigation
{
    public class Node
    {
        public int gCost, hCost;
        public bool Obstacle;
        public readonly Vector2 WorldPosition;
        public readonly int GridX, GridY;
        public Node Parent;

        public Node(bool obstacle, Vector2 worldPos, int gridX, int gridY)
        {
            Obstacle = obstacle;
            WorldPosition = worldPos;
            GridX = gridX;
            GridY = gridY;
        }

        public int FCost
        {
            get
            {
                return gCost + hCost;
            }

        }

        public void SetObstacle(bool obstacle)
        {
            Obstacle = obstacle;
        }
    }
}
