using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public class EnemySpawner : MonoBehaviour, IUnitSpawner
    {
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _spawnRate;
        [SerializeField]
        private byte _spawnMaxCount;

        [SerializeField]
        private EnemyFactory _factory;

        private bool _spawning;
        private float _timer;
        private byte _counter;

        private void Update()
        {
            if (_spawning || _counter >= _spawnMaxCount) return;

            if (_timer > 0) _timer -= Time.deltaTime;
            else
            {
                var unit = SpawnUnit();
                _counter++;
                _timer = _spawnRate;
            }
        }

        public UnitView SpawnUnit()
        {
            return _factory.GetRandomUnit();
        }

        public void Enable(bool isSpawning)
        {
            _spawning = isSpawning;
        }
    }
}