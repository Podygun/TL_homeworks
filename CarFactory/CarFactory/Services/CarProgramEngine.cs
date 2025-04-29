using CarFactory.Data;
using CarFactory.Domain;
using Spectre.Console;

namespace CarFactory.Services;

internal sealed class CarProgramEngine
{
    private readonly List<ICar> _createdCars = [];

    public void Run()
    {
        AnsiConsole.Write(
            new FigletText( "Car Factory" )
                .LeftJustified()
                .Color( Color.Red ) );

        while ( true )
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title( "Select action:" )
                    .PageSize( 10 )
                    .AddChoices( new[]
                    {
                        "Create new car",
                        "View created cars",
                        "Exit"
                    } ) );

            switch ( choice )
            {
                case "Create new car":
                    CreateCarFlow();
                    break;
                case "View created cars":
                    ShowCreatedCars();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private void ShowCreatedCars()
    {
        if ( _createdCars.Count == 0 )
        {
            AnsiConsole.MarkupLine( "[yellow]No cars created yet![/]" );
            return;
        }

        foreach ( var car in _createdCars )
        {
            car.DisplayConfiguration();
            AnsiConsole.WriteLine();
        }
    }

    private void CreateCarFlow()
    {
        try
        {
            string model = GetSelection( "Select model:", CarData.Models );
            string bodyType = GetSelection( "Select body type:", CarData.BodyTypes );
            string engine = GetSelection( "Select engine:", CarData.EngineTypes );
            string transmission = String.Empty;

            if ( engine.ToLower().Contains( "электр" ) )
            {
                transmission = "АКПП";

            }

            string transmission = GetSelection( "Select transmission:", CarData.TransmissionTypes );
            string color = GetSelection( "Select color:", CarData.Colors );
            string wheelPosition = GetSelection( "Select wheel position:", CarData.WheelDrive );
            int gearCount = GetSelection( "Select gear counts:", CarData.GearCounts );

            ICar car = CarFactory.CreateCar( model, bodyType, engine, transmission, color, wheelPosition, gearCount );
            _createdCars.Add( car );

            AnsiConsole.MarkupLine( "[green]Car created successfully![/]" );
            car.DisplayConfiguration();
        }
        catch ( Exception ex )
        {
            AnsiConsole.MarkupLine( $"[red]Error: {ex.Message}[/]" );
        }
    }

    private static string GetSelection( string title, string[] options )
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title( title )
                .PageSize( 10 )
                .AddChoices( options ) );
    }

    private static int GetSelection( string title, int[] options )
    {
        string[] stringOptions = options.Select( x => x.ToString() ).ToArray();

        string selected = GetSelection( title, stringOptions );

        return int.Parse( selected );
    }
}
