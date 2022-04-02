using System;
using UnityEngine;

namespace Core.Units
{
    public class RangeCheckingSystem : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layer;

        private Ray _ray;
        private RaycastHit[] _raycastResults = new RaycastHit[3];
        private Collider[] _collidersResults = new Collider[3];
        private int _hits;
        private bool _checking;
        private float _range;

        public event Action<UnitView> EnemyDetected;

        private void FixedUpdate()
        {
            if (!_checking) return;

            if (AcquireTarget(out UnitView target) && CheckObstacles(target))
            {
                EnemyDetected?.Invoke(target);
            }           
        }
        private bool AcquireTarget(out UnitView target)
        {
            _hits = Physics.OverlapSphereNonAlloc(transform.position, _range, _collidersResults, _layer.value);
            if (_hits > 0)
            {
                return _collidersResults[0].TryGetComponent(out target);
            }
            target = null;
            return false;
        }
        private bool CheckObstacles(UnitView unit)
        {
            _ray = new Ray(transform.position, unit.transform.position - transform.position);
            _hits = Physics.RaycastNonAlloc(_ray, _raycastResults, Mathf.Infinity, ~Constants.IgnoreRaycastLayerMask);
            if (_hits > 0)
            {
                // Sorting targets by distance
                Array.Sort(_raycastResults, (RaycastHit x, RaycastHit y) =>
                {
                    if (y.collider == null) return -1;
                    return x.distance.CompareTo(y.distance);
                });
                return _raycastResults[0].transform.TryGetComponent<UnitView>(out var target);
            }
            return false;
        }

        public void EnableChecking(bool isChecking)
        {
            _checking = isChecking;
        }
        public void SetRange(float range)
        {
#if UNITY_EDITOR
            Debug.Assert(range > 0, "Range cannot be less than zero!");
#endif
            _range = range;
        }
    }
}