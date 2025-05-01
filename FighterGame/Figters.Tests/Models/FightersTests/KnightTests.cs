using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class KnightTests : BaseMockFighter
{
    private readonly Knight _knight;

    public KnightTests()
    {
        _knight = new Knight( "Test Knight", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );
    }

    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Act
        int damage = _knight.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {
        // Arrange
        int damageToAttack = 30;
        double knightReduction = damageToAttack * 0.1; // Блокировка части урона Knight

        _knight.TakeDamage( damageToAttack );

        // Act
        int actualHealth = _knight.GetCurrentHealth();
        int expectedHealth = ( int )( 100 - ( damageToAttack - knightReduction - _knight.CalculateArmor() ) );

        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Act
        int currentArmor = _knight.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }
}
