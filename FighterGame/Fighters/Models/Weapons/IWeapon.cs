namespace Fighters.Models.Weapons;

public interface IWeapon
{
    public int Damage { get; }

    public double CriticalMultiplier { get; }

    public double CriticalChance { get; }
}