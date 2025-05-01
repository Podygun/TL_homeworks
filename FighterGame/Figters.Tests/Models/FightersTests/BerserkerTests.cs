using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class BerserkerTests : BaseMockFighter
{
    private readonly Berserker _berserker;

    public BerserkerTests()
    {
        _berserker = new Berserker( "Test Berserker", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );
    }


    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Act
        int damage = _berserker.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {
        // Arrange
        int damageToAttack = 30;
        double knightReduction = damageToAttack * 0.2; // Блокировка части урона Berserker

        _berserker.TakeDamage( damageToAttack );

        // Act
        int actualHealth = _berserker.GetCurrentHealth();
        int expectedHealth = ( int )( 100 - ( damageToAttack - knightReduction - _berserker.CalculateArmor() ) );


        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Act
        int currentArmor = _berserker.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }

}
