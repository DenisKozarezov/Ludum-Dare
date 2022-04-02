using UnityEngine;

namespace Core.Units
{
    public class EnemySpawner : MonoBehaviour
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

        private void Start()
        {
            EnableSpawn(true);
        }
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

        private UnitView SpawnUnit()
        {
            return _factory.GetRandomUnit();
        }

        public void EnableSpawn(bool isSpawning)
        {
            _spawning = isSpawning;
        }
    }
}