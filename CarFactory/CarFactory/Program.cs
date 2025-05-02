using CarFactory.Services;

namespace CarFactory;

internal sealed class Program
{
    public static void Main( string[] args )
    {
        CarProgramEngine engine = new();
        engine.Run();
    }
}