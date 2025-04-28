using CarFactory.Services;

namespace CarFactory;

internal sealed class Program
{
    public static void Main( string[] args )
    {
        try
        {
            CarProgramEngine engine = new();
            engine.Run();
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка: {ex.Message}" );
        }
    }
}
