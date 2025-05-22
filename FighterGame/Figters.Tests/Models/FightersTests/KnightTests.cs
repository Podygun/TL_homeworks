using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class KnightTests : BaseMockFighter
{
    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Arrange

        Knight knight = new( "Test Knight", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int damage = knight.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {
        // Arrange
        Knight knight = new( "Test Knight", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        int damageToAttack = 30;
        double knightReduction = damageToAttack * 0.1; // Блокировка части урона Knight

        knight.TakeDamage( damageToAttack );

        // Act
        int actualHealth = knight.GetCurrentHealth();
        int expectedHealth = ( int )( 100 - ( damageToAttack - knightReduction - knight.CalculateArmor() ) );

        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Arrange

        Knight knight = new( "Test Knight", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int currentArmor = knight.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }
}
