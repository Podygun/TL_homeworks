using Spectre.Console;

namespace CarFactory;

internal sealed class Program
{
    public static void Main( string[] args )
    {
        SelectLanguage();
        CarProgramEngine engine = new();
        engine.Run();
    }

    private static void SelectLanguage()
    {
        string language = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title( "Select language / Выберите язык:" )
                .AddChoices( [ "English", "Русский" ] ) );

        if ( language.Equals( "Русский" ) )
        {
            Localizator.SetCulture( "ru-RU" );
        }
        else
        {
            Localizator.SetCulture( "en-US" );
        }
    }
}