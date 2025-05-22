namespace Figters.Tests.Models.FightersTests.BaseFighterTests;

public sealed class FighterBaseTests
{
    [Fact]
    public void IsAlive_ShouldReturnTrue_WhenHealthIsAboveZero()
    {
        //Arrange
        TestFighter fighter = new( "Test Fighter", 100 );

        // Act
        bool isAlive = fighter.IsAlive();

        // Assert
        Assert.True( isAlive );
    }

    [Fact]
    public void GetCurrentHealth_ShouldReturnInitialHealth()
    {
        //Arrange
        TestFighter fighter = new( "Test Fighter", 100 );

        // Act
        int currentHealth = fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( 100, currentHealth );
    }

    [Fact]
    public void GetMaxHealth_ShouldReturnMaxHealth()
    {
        //Arrange
        TestFighter fighter = new( "Test Fighter", 100 );

        // Act
        int maxHealth = fighter.GetMaxHealth();

        // Assert
        Assert.Equal( 100, maxHealth );
    }

    [Fact]
    public void ResetState_ShouldResetHealthToMaxHealth()
    {
        //Arrange
        TestFighter fighter = new( "Test Fighter", 100 );

        // Arrange
        fighter.TakeDamage( 50 );

        // Act
        fighter.ResetState();
        int currentHealth = fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( 100, currentHealth ); // Проверяем, что здоровье восстановлено
    }
}
