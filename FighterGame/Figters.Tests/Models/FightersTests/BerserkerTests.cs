using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests;

public sealed class BerserkerTests : BaseMockFighter
{

    [Fact]
    public void CalculateDamage_ShouldReturnCorrectDamage()
    {
        // Arrange
        Berserker berserker = new Berserker( "Test Berserker", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int damage = berserker.CalculateDamage();

        // Assert
        Assert.Equal( 25, damage ); // 20 (оружие) + 5 (раса)
    }


    [Fact]
    public void TakeDamage_ShouldReduceHealthCorrectly()
    {
        // Arrange
        Berserker berserker = new Berserker( "Test Berserker", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        int damageToAttack = 30;
        double knightReduction = damageToAttack * 0.2; // Блокировка части урона Berserker

        berserker.TakeDamage( damageToAttack );

        // Act
        int actualHealth = berserker.GetCurrentHealth();
        int expectedHealth = ( int )( 100 - ( damageToAttack - knightReduction - berserker.CalculateArmor() ) );


        // Assert
        Assert.Equal( expectedHealth, actualHealth );
    }


    [Fact]
    public void CalculateArmor_ShouldReturnCorrectArmor()
    {
        // Arrange
        Berserker berserker = new Berserker( "Test Berserker", _mockRace.Object, _mockWeapon.Object, _mockArmor.Object );

        // Act
        int currentArmor = berserker.CalculateArmor();

        // Assert
        Assert.Equal( 15, currentArmor ); // 10 (броня расы) + 5 (броня брони)
    }

}
