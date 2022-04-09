using System.Collections.Generic;
using UnityEngine;

namespace Core.Navigation
{
    public class Pathfinder : MonoBehaviour, IPathfinder
    {
        private NavigationGrid _grid;      
        private IWalkable _walkable;
        private Node _startNode, _targetNode;
        private LinkedListNode<Node> _currentNode;
        private bool _isStopped;
        private LinkedList<Node> _path = new LinkedList<Node>();

        private Vector2 Position => (Vector2)gameObject.transform.position + Vector2.down * _walkable.Size.y / 2;

        public bool PathValid => _path.Count != 0 && _currentNode != null;
        public bool PathComplete => PathValid && _currentNode.Equals(_targetNode);
        public bool IsStopped => _isStopped;

        public void Awake()
        {
            _grid = NavigationGrid.Instance;
            _walkable = GetComponent<IWalkable>();
        }

        public LinkedList<Node> CalculatePath(Vector2 startPos, Vector2 targetPos)
        {
            _startNode = _grid.NodeFromWorldPoint(startPos);
            _targetNode = _grid.NodeFromWorldPoint(targetPos);

            if (_targetNode.Obstacle || _startNode.Equals(_targetNode))
            {
                return new LinkedList<Node>();
            }

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            LinkedList<Node> path = new LinkedList<Node>();
            openSet.Add(_startNode);

            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost <= node.FCost)
                    {
                        if (openSet[i].hCost < node.hCost) node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node.Equals(_targetNode))
                {
                    path = RetracePath(_startNode, node);
                    break;
                }

                foreach (Node neighbor in _grid.GetNeighbors(node))
                {
                    if (neighbor.Obstacle || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newCostToNeighbor = node.gCost + GetDistance(node, neighbor);
                    if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, _targetNode);
                        neighbor.Parent = node;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }         
            }
            return path;
        }
        private LinkedList<Node> RetracePath(Node startNode, Node endNode)
        {
            LinkedList<Node> path = new LinkedList<Node>();
            Node currentNode = endNode;

            while (!currentNode.Equals(startNode))
            {
                path.AddLast(currentNode);
                if (currentNode.Parent == null) break;
                currentNode = currentNode.Parent;           
            }
            return path;
        }
        private int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
        public void SetDestination(Vector2 destination)
        {
            Stop();

            _path = CalculatePath(gameObject.transform.position, destination);
            if (_path.Count > 0)
            {
                _currentNode = _path.Last;
                _grid.DrawPath(_path);
            }
        }
        public void Move()
        {
            if (!PathValid || PathComplete) return;

            Vector2 direction = _currentNode.Value.WorldPosition - Position;
            float sqrDistance = direction.sqrMagnitude;
            if (sqrDistance <= 0.01f)
            {
                _currentNode = _currentNode.Previous;
            }

            _walkable.Translate(direction.normalized);
        }
        public void Stop()
        {
            _isStopped = true;
            _currentNode = null;
            _startNode = null;
            _targetNode = null;
            _path.Clear();
        }
    }
}