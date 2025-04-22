namespace Fighters.Models.Weapons;

class BattleAxe : WeaponBase
{
    public override int Damage => 20;

    public override double CriticalMultiplier => 2.0d;

    public override double CriticalChance => 0.1d;
}
