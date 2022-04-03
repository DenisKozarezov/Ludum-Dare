namespace Core
{
    public static class Constants
    {
        public const int IgnoreRaycastLayerMask = 1 << 2;
        public const int PlayerLayerMask = 1 << 3;
        public const int EnemyLayerMask = 1 << 6;
        public const int EnvironmentLayerMask = 1 << 7;

        public const float WanderRange = 5f;
        public const float AttackRadius = 1f;
        public const float AggressionRadius = 3f;

        public const float DeathTimer = 5f;
    }
}
