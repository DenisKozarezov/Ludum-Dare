using System;
using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public class UnitSpawner : MonoBehaviour, IUnitSpawner
    {
        [Header("Options")]
        [SerializeField]
        private UnitOwner _owner;
        [SerializeField, Min(0)]
        private float _spawnRate;
        [SerializeField]
        private byte _spawnMaxCount;
        [SerializeField]
        public float _energySpawnRate = 0f;

        [SerializeField]
        private UnitFactory _factory;

        private bool _spawning;
        private float _timer;
        private byte _counter;

        public UnitOwner Owner => _owner;
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
                unit.UnitState.Owner = _owner;
                _counter++;
                _timer = _spawnRate + _energySpawnRate;
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
        public void SetEnergySpawnRate(byte energySpawnRate)
        {
            _energySpawnRate = energySpawnRate;
        }
        public void SetSpawnRate(byte rate)
        {
            _spawnRate = rate;
        }
        public void SetSpawnAmount(byte amount)
        {
            _spawnMaxCount = amount;
        }
    }
}