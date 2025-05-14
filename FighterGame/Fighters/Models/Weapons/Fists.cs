namespace Fighters.Models.Weapons;

public class Fists : WeaponBase
{
    public override int Damage => 10;

    public override double CriticalMultiplier => 10d;

    public override double CriticalChance => 0.2d;
}