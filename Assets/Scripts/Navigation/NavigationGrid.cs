using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Navigation
{
    public class NavigationGrid : MonoBehaviour
    {
        private static NavigationGrid _instance;
        public static NavigationGrid Instance => _instance;

        [SerializeField]
        private Vector2 _gridSize;
        [SerializeField]
        private Vector2 _offset;
        [SerializeField]
        private float _nodeRadius;
        [SerializeField]
        private Tilemap _obstacleTilemap;
     
        private Vector2 _worldBottomLeft;
        private Node[,] _grid;
        private float NodeDiameter => _nodeRadius * 2;
        private int _gridSizeX, _gridSizeY;

        public float NodeRadius => _nodeRadius;

        private IEnumerable<Node> Path;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            _gridSizeX = Mathf.RoundToInt(_gridSize.x / NodeDiameter);
            _gridSizeY = Mathf.RoundToInt(_gridSize.y / NodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            _worldBottomLeft = (Vector2)transform.position - Vector2.right * _gridSize.x / 2 - Vector2.up * _gridSize.y / 2 + _offset;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector2 worldPoint = _worldBottomLeft + Vector2.right * (x * NodeDiameter + _nodeRadius) + Vector2.up * (y * NodeDiameter + _nodeRadius);
                    bool isObstacle = _obstacleTilemap.HasTile(_obstacleTilemap.WorldToCell(worldPoint));       
                    
                    _grid[x, y] = new Node(isObstacle, worldPoint, x, y);
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

        public Node NodeFromWorldPoint(Vector2 worldPosition)
        {        
            float deltaX = worldPosition.x - transform.position.x;
            float deltaY = worldPosition.y - transform.position.y;

            float percentX = (deltaX + _gridSize.x / 2) / _gridSize.x;
            float percentY = (deltaY + _gridSize.y / 2) / _gridSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
            return _grid[x, y];
        }
        public void DrawPath(IEnumerable<Node> path)
        {
            Path = path;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawSolidRectangleWithOutline(new Rect((Vector2)transform.position - _gridSize / 2 + _offset, _gridSize), new Color(0,0,0,0), Color.green);

            if (_grid != null)
            {
                foreach (Node node in _grid)
                {
                    Gizmos.color = node.Obstacle ? Color.red : Color.white;                 
                    if (Path != null && Path.Contains(node)) Gizmos.color = Color.green;
                    
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * _nodeRadius);
                }
            }
        }
#endif
    }
}