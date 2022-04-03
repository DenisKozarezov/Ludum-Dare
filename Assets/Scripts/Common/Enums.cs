namespace Core.Units
{
    public enum UnitType : byte
    {
        Default = 0,
        Hero = 1,
    }
    public enum UnitRank : byte
    {
        Recruit = 0,
        Commander = 1,
        General = 2,
    }
    public enum RaceType : byte
    {
        Default = 0,
        Human = 1,
        Orc = 2,
        Elf = 4,
        Undead = 8,
        Elemental = 16,
    }
    public enum Specialization : byte
    {
        Default = 0,
        Warrior = 1,
        Mage = 2,
        Archer = 4,
    }
    public enum UnitOwner : byte
    {
        Neutral = 0,
        Enemy = 1,
        Player = 2,
    }
}

namespace Core.UI
{
    public enum MenuStates
    {
        Menu,
        Settings,
        Credits,
    }
}

namespace Core
{
    public enum FadeMode : byte { In, Out }
}