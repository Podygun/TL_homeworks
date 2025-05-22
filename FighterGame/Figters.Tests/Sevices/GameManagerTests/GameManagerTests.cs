using Fighters;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Figters.Tests.Sevices.GameManagerTests;

public class GameManagerTests : IDisposable
{
    private MockGameManager _gameManager;
    private StringWriter _stringWriter;
    private StringReader _stringReader;

    public GameManagerTests()
    {
        _gameManager = new MockGameManager();
        _stringWriter = new StringWriter();
        Console.SetOut( _stringWriter );
    }

    public void Dispose()
    {
        _stringWriter.Dispose();
        Console.SetOut( Console.Out );
    }

    [Fact]
    public void TestChooseFighterClass()
    {
        // Arrange
        MockGameManager mockManager = new MockGameManager();
        Human testRace = new Human();
        Fists testWeapon = new Fists();
        NoArmor testArmor = new NoArmor();

        // Act
        FighterBase fighter = mockManager.CreateFighter( "Test", testRace, testWeapon, testArmor );

        // Assert
        Assert.IsType<Knight>( fighter );
        Assert.Equal( "Test", fighter.Name );
    }

}
