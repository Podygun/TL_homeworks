using Fighters;

namespace Figters.Tests.Sevices.GameManagerTests;

public class GameManagerTests : IDisposable
{
    private GameManager _gameManager;
    private StringWriter _stringWriter;
    private StringReader _stringReader;

    public GameManagerTests()
    {
        _gameManager = new GameManager();
        _stringWriter = new StringWriter();
        Console.SetOut( _stringWriter );
    }

    public void Dispose()
    {
        _stringWriter.Dispose();
        Console.SetOut( Console.Out );
    }


    [Fact]
    public void CreateFighter_ShouldReturnCorrectFighter()
    {

    }


}
