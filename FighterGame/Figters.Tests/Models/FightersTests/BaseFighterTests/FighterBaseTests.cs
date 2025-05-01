namespace Figters.Tests.Models.FightersTests.BaseFighterTests;

public sealed class FighterBaseTests
{
    //Arrange
    TestFighter _fighter = new( "Test Fighter", 100 );


    [Fact]
    public void IsAlive_ShouldReturnTrue_WhenHealthIsAboveZero()
    {
        // Act
        bool isAlive = _fighter.IsAlive();

        // Assert
        Assert.True( isAlive );
    }

    [Fact]
    public void GetCurrentHealth_ShouldReturnInitialHealth()
    {
        // Act
        int currentHealth = _fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( 100, currentHealth );
    }

    [Fact]
    public void GetMaxHealth_ShouldReturnMaxHealth()
    {
        // Act
        int maxHealth = _fighter.GetMaxHealth();

        // Assert
        Assert.Equal( 100, maxHealth );
    }

    [Fact]
    public void ResetState_ShouldResetHealthToMaxHealth()
    {
        // Arrange
        _fighter.TakeDamage( 50 );

        // Act
        _fighter.ResetState();
        int currentHealth = _fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( 100, currentHealth ); // Проверяем, что здоровье восстановлено
    }
}
