namespace AdvancedDictionary;

static class ConsoleHelper
{
    public static string ReadString()
    {
        string? inputString = Console.ReadLine()?.Trim();
        while ( String.IsNullOrEmpty( inputString ) )
        {
            Console.Write( "Пустая строка запрещена, повторите ввод: " );
            inputString = Console.ReadLine();
        }
        return inputString;
    }

    public static void SetErrorColor()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    public static void ResetColor()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.ResetColor();
    }

    public static void SetSuccessColor()
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public static void SetWarningColor()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    public static void SetInfoColor()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
    }

    public static void PrintError( string message )
    {
        SetErrorColor();
        Console.WriteLine( message );
        ResetColor();
    }

    public static void PrintWarning( string message )
    {
        SetWarningColor();
        Console.WriteLine( message );
        ResetColor();
    }

    public static void PrintNotify( string message )
    {
        SetInfoColor();
        Console.WriteLine( message );
        ResetColor();
    }

    public static void PrintSuccess( string message )
    {
        SetSuccessColor();
        Console.WriteLine( message );
        ResetColor();
    }

}
