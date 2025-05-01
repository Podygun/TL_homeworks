namespace Figters.Tests.Models.WeaponsTests.BaseWeaponTests;

public sealed class WeaponBaseTests
{
    private WeaponEntityNoCriticalChance _weaponNoCrit = new();

    private WeaponEntityAlwaysCriticalChance _weaponAlwaysCrit = new();

    [Fact]
    public void TestWeaponEntity_ShouldReturnCorrectValues()
    {
        // Assert
        Assert.Equal( 20, _weaponNoCrit.Damage );
        Assert.Equal( 0.0d, _weaponNoCrit.CriticalMultiplier );
        Assert.Equal( 0.0d, _weaponNoCrit.CriticalChance );
    }


    [Fact]
    public void CalculateDamage_ShouldReturnDamageWithNoCrit()
    {
        // Act
        int damage = _weaponNoCrit.CalculateDamage();

        // Assert
        Assert.InRange( damage, 17, 23 ); // +-15% от 20
    }

    [Fact]
    public void CalculateDamage_ShouldReturnDamageWithCrit()
    {
        // Act
        int damage = _weaponAlwaysCrit.CalculateDamage();

        // Assert
        Assert.InRange( damage, 34, 46 ); // 20 * 2 = 40 (+- 15%)
    }
}
