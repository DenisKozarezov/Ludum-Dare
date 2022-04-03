using System;
using UnityEngine;
using UnityEngine.Events;
using Core.Units;

namespace Core.Services
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _delayBeforeWave;
        [SerializeField]
        private byte _increasePerWave;

        private ushort _enemiesCount;

        [SerializeField]
        private EnemySpawner[] _spawners;

        private byte _wavesCount;

        public UnityEvent WaveBegin;
        public UnityEvent WaveEnded;

        private void Start()
        {
            for (int i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].UnitManufactured += OnUnitManufactured;
            }
        }

        private void OnUnitManufactured(UnitView unit)
        {
            _enemiesCount++;
            unit.Died += () =>
            {
                _enemiesCount--;
                if (_enemiesCount == 0) EndWave();
            };
        }
        private void ActivateSpawners(bool isActive)
        {
            for (int i = 0; i < _spawners.Length; i++)
            {
                byte increasedRate = (byte)(_spawners[i].SpawnMaxCount + _increasePerWave);
                _spawners[i].SetSpawnAmount(increasedRate);
                _spawners[i].Enable(isActive);
            }
        }
        public void StartWave()
        {
#if UNITY_EDITOR
            Debug.Log($"<b><color=green>[WAVES]</color></b>: New wave <b><color=yellow>incoming</color></b>. Waves passed: <b><color=yellow>{_wavesCount}</color></b>.");
#endif

            _wavesCount++;
            ActivateSpawners(true);
            WaveBegin?.Invoke();
        }
        private void EndWave()
        {
            ActivateSpawners(false);
            WaveEnded?.Invoke();
        }
    }
}