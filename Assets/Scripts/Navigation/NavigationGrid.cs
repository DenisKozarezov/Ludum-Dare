using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Navigation
{
    public class NavigationGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _gridSize;
        [SerializeField]
        private float _nodeRadius;
        [SerializeField]
        private Tilemap _obstacleTilemap;
     
        private Vector2 _worldBottomLeft;
        private Node[,] _grid;
        private float NodeDiameter => _nodeRadius * 2;
        private int _gridSizeX, _gridSizeY;

        [HideInInspector]
        public List<Node> Path;

        private void Awake()
        {
            _gridSizeX = Mathf.RoundToInt(_gridSize.x / NodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridSize.y / NodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            _worldBottomLeft = (Vector2)transform.position - Vector2.right * _gridSize.x / 2 - Vector2.up * _gridSize.y / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector2 worldPoint = _worldBottomLeft + Vector2.right * (x * NodeDiameter + _nodeRadius) + Vector2.up * (y * NodeDiameter + _nodeRadius);
                    _grid[x, y] = new Node(false, worldPoint, x, y);

                    if (_obstacleTilemap.HasTile(_obstacleTilemap.WorldToCell(_grid[x, y].WorldPosition)))
                        _grid[x, y].SetObstacle(true);
                    else
                        _grid[x, y].SetObstacle(false);
                }
            }
        }

        public List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            // Top neighbor
            if (node.GridX >= 0 && node.GridX < _gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX, node.GridY + 1]);

            // Bottom neighbor
            if (node.GridX >= 0 && node.GridX < _gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX, node.GridY - 1]);

            // Right neighbor
            if (node.GridX + 1 >= 0 && node.GridX + 1 < _gridSizeX && node.GridY >= 0 && node.GridY < _gridSizeY)
                neighbors.Add(_grid[node.GridX + 1, node.GridY]);

            // Left neighbor
            if (node.GridX - 1 >= 0 && node.GridX - 1 < _gridSizeX && node.GridY >= 0 && node.GridY < _gridSizeY)
                neighbors.Add(_grid[node.GridX - 1, node.GridY]);

            // Diagonal movement
            if (node.GridX + 1 >= 0 && node.GridX + 1 < _gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX + 1, node.GridY + 1]);
            if (node.GridX + 1 >= 0 && node.GridX + 1 < _gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX + 1, node.GridY - 1]);
            if (node.GridX - 1 >= 0 && node.GridX - 1 < _gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX - 1, node.GridY + 1]);
            if (node.GridX - 1 >= 0 && node.GridX - 1 < _gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < _gridSizeY)
                neighbors.Add(_grid[node.GridX - 1, node.GridY - 1]);

            return neighbors;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x - 1 + (_gridSizeX / 2));
            int y = Mathf.RoundToInt(worldPosition.y + (_gridSizeY / 2));
            return _grid[x, y];
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_gridSize.x, _gridSize.y, 1));

            if (_grid != null)
            {
                foreach (Node n in _grid)
                {
                    if (n.Obstacle)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.white;

                    if (Path != null && Path.Contains(n)) Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * _nodeRadius);
                }
            }
        }
#endif
    }
}