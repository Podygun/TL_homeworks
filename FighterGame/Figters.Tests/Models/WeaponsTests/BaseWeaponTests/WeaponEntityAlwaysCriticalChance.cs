using Fighters.Models.Weapons;

namespace Figters.Tests.Models.WeaponsTests.BaseWeaponTests;

public sealed class WeaponEntityAlwaysCriticalChance : WeaponBase
{
    public override int Damage => 20;

    public override double CriticalMultiplier => 2.0d;

    public override double CriticalChance => 1.0d;
}
