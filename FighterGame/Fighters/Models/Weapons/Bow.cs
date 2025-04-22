namespace Fighters.Models.Weapons;

public class Bow : WeaponBase
{
    public override int Damage => 15;

    public override double CriticalMultiplier => 3.0d;

    public override double CriticalChance => 0.05d;
}
