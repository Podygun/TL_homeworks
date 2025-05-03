using Fighters;
using Fighters.Models.Fighters;

namespace Figters.Tests.Sevices.GameManagerTests;

public sealed class MockGameManager : GameManager
{
    public static string InputString( string message )
    {
        return GameManager.InputString( message );
    }

    public static int InputInt( string message, int min, int max )
    {
        return GameManager.InputInt( message, min, max );
    }

}