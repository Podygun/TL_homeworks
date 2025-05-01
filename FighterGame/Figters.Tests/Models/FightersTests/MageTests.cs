using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class MageTests : BaseMockFighter
{
    private readonly Mage _mage;

    public MageTests()
    {
        _mage = new Mage( "Test Berserker", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );
    }


    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Act
        int damage = _mage.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {
        // Arrange
        int damageToAttack = 30;

        _mage.TakeDamage( damageToAttack );

        // Act
        int actualHealth = _mage.GetCurrentHealth();
        int expectedHealth = 100 - ( damageToAttack - _mage.CalculateArmor() );

        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Act
        int currentArmor = _mage.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }

}