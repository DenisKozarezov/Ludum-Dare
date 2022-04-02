namespace Core.Models
{
    public class UnitState
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public float NormalizedHealth => Health / (float)MaxHealth;
    }
}