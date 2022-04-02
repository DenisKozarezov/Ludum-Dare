using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public class PlayerSpawner : MonoBehaviour, IUnitSpawner
    {
        [Header("Options")]
        [SerializeField]
        private byte _spawnMaxCount;

        [SerializeField]
        private PlayerFactory _factory;

        private bool _spawning;

        public UnitView SpawnUnit()
        {
            return _spawning ? _factory.GetRandomUnit() : null;
        }

        public void Enable(bool isSpawning)
        {
            _spawning = isSpawning;
        }
    }
}