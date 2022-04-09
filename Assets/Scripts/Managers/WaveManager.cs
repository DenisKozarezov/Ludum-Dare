using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Core.Units;

namespace Core.Services
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private EnergyDistribution _energyDistribution;
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _delayBeforeWave;
        [SerializeField]
        private byte _increasePerWave;

        private bool _enable = true;
        private ushort _enemiesCount;
        private byte _wavesCount;

        [SerializeField]
        private UnitSpawner[] _spawners;

        [Space]
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
            if (unit is IEnemy) _enemiesCount++;
            _energyDistribution.SpendEnergy(unit.UnitData.Stats.Cost);
            unit.Died += () =>
            {
                if (unit is IEnemy) _enemiesCount--;
                if (_enemiesCount == 0) EndWave();         
            };
        }
        private async void OnWaveEnded()
        {
            if (!_enable) return;
            await Task.Delay(TimeSpan.FromSeconds(_delayBeforeWave));
            StartWave();
        }
        private void UpgradeSpawners(byte addSpawnRate, byte addSpawnAmount)
        {
           foreach (var spawner in _spawners)
           {
                switch (spawner.Owner)
                {
                    case UnitOwner.Player:
                        spawner.SetSpawnRate((byte)(spawner.SpawnMaxCount + addSpawnRate));
                        spawner.SetSpawnAmount((byte)(spawner.SpawnMaxCount + addSpawnAmount));
                        break;
                }
           }
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
            _enable = false;
        }
    }
}