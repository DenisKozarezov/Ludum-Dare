namespace Core.Units
{
    public struct UnitRecievedDamageArgs
    {
        public UnitView Target;
        public UnitView Source;
        public float Damage;
    }

    public struct UnitUpgradeArgs
    {
        public float AddMaxHealth;
        public float AddDamage;
        public float AddMovementSpeed;
        public float AddAttackSpeed;
        public float AddHpRegeneration;
    }
}
