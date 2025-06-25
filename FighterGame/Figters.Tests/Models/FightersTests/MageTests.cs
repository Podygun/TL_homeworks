using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class MageTests : BaseMockFighter
{
    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Arrange
        Mage mage = new( "Test Mage", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int damage = mage.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {

        // Arrange
        Mage mage = new( "Test Mage", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        int damageToAttack = 30;

        mage.TakeDamage( damageToAttack );

        // Act
        int actualHealth = mage.GetCurrentHealth();
        int expectedHealth = 100 - ( damageToAttack - mage.CalculateArmor() );

        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Arrange
        Mage mage = new( "Test Mage", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int currentArmor = mage.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }

}