namespace Core.Units
{
    public struct UnitRecievedDamageArgs
    {
        public UnitView Target;
        public UnitView Source;
        public ushort Damage;
    }

    public class UnitStatuses
    {
        public bool Stunned;
        public bool Rooted;
        public bool Invulnerable;
    }
}
