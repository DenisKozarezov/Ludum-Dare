using System;
using UnityEngine;

namespace Core.Navigation
{
    public class Node : IEquatable<Node>
    {
        public int gCost, hCost;
        public readonly bool Obstacle;
        public readonly Vector2 WorldPosition;
        public readonly int GridX, GridY;
        public Node Parent;

        public int FCost => gCost + hCost;

        public Node(bool obstacle, Vector2 worldPos, int gridX, int gridY)
        {
            Obstacle = obstacle;
            WorldPosition = worldPos;
            GridX = gridX;
            GridY = gridY;
        }

        public bool Equals(Node other)
        {
            return GridX == other.GridX && GridY == other.GridY;
        }
    }
}
