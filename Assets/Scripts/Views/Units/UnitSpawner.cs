using System;
using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public class UnitSpawner : MonoBehaviour, IUnitSpawner
    {
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _spawnRate;
        [SerializeField]
        private byte _spawnMaxCount;

        [SerializeField]
        private UnitFactory _factory;

        private bool _spawning;
        private float _timer;
        private byte _counter;

        public byte SpawnMaxCount => _spawnMaxCount;

        public event Action<UnitView> UnitManufactured;

        private void Update()
        {
            if (!_spawning || _counter >= _spawnMaxCount)
            {
                return;
            }

            if (_timer > 0) _timer -= Time.deltaTime;
            else
            {
                var unit = SpawnUnit();
                unit.transform.position = transform.position;
                _counter++;
                _timer = _spawnRate;
                UnitManufactured?.Invoke(unit);
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
        public void SetSpawnAmount(byte amount)
        {
            _spawnMaxCount = amount;
        }
    }
}