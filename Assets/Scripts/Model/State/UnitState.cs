using Core.Units;

namespace Core.Models
{
    public class UnitState
    {
        public float Health { get; set; }
        public float NormalizedHealth => Health / UnitStats.MaxHealth;
        public UnitOwner Owner { get; set; }
        public UnitStats UnitStats;
    }
}