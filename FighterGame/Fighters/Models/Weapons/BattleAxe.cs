namespace Fighters.Models.Weapons;

public class BattleAxe : WeaponBase
{
    public override int Damage => 25;

    public override double CriticalMultiplier => 2.0d;

    public override double CriticalChance => 0.1d;
}
