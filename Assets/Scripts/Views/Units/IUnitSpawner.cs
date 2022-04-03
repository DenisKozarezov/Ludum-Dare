using System;

namespace Core.Units
{
    public interface IUnitSpawner
    {
        UnitView SpawnUnit();
        void Enable(bool isSpawning);
        void SetSpawnAmount(byte amount);
        event Action<UnitView> UnitManufactured;
    }
}