namespace Fighters.Models.Weapons;

public class Warhammer : WeaponBase
{
    public override int Damage => 20;

    public override double CriticalMultiplier => 2.2d;

    public override double CriticalChance => 0.1d;
}
