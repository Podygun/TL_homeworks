using Fighters.Models.Weapons;

namespace Figters.Tests.Models.WeaponsTests.BaseWeaponTests;

public sealed class WeaponEntityNoCriticalChance : WeaponBase
{
    public override int Damage => 20;

    public override double CriticalMultiplier => 0.0d;

    public override double CriticalChance => 0.0d;
}
