using System;

namespace Core.Units
{
    public interface IUnitSpawner
    {
        UnitView SpawnUnit();
        void Enable(bool isSpawning);
        event Action<UnitView> UnitManufactured;
    }
}