using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Core.Units;
using System.Threading;

namespace Core.Services
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _delayBeforeWave;
        [SerializeField]
        private byte _increasePerWave;

        private bool _spawning;
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
            WaveEnded.AddListener(OnWaveEnded);
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
        private async void OnWaveEnded()
        {
            if (!_spawning) return;
            await Task.Delay(TimeSpan.FromSeconds(_delayBeforeWave));
            StartWave();
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
            _wavesCount++;

#if UNITY_EDITOR
            Debug.Log($"<b><color=green>[WAVES]</color></b>: New wave <b><color=yellow>incoming</color></b>. Current wave: <b><color=yellow>{_wavesCount}</color></b>.");
#endif
            
            ActivateSpawners(true);
            WaveBegin?.Invoke();
        }
        public void EndWave()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[WAVES]</color></b>: The wave <b><color=yellow>has passed</color></b>.");
#endif

            ActivateSpawners(false);
            WaveEnded?.Invoke();
        }
        public void EndGame()
        {
            ActivateSpawners(false);
            _spawning = false;
        }
    }
}