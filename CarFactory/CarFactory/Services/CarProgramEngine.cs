using CarFactory.Data;
using CarFactory.Domain;
using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;
using Spectre.Console;
using static CarFactory.Data.CarData;

namespace CarFactory.Services;

internal sealed class CarProgramEngine
{
    private readonly List<ICar> _createdCars = [];
    private bool _isExit = false;

    public void Run()
    {
        while ( !_isExit )
        {
            try
            {
                EngineLoop();
            }
            catch ( Exception ex )
            {
                AnsiConsole.MarkupLine( $"[red]{Localizator.ErrorPrefix} {ex.Message}![/]" );
            }
        }

    }

    private void EngineLoop()
    {
        AnsiConsole
            .Write( new FigletText( "Car Factory" )
            .LeftJustified()
            .Color( Color.Red ) );
        while ( true )
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title( $"{Localizator.SelectAction}" )
                    .PageSize( 10 )
                    .AddChoices(
                    [
                        Localizator.CreateCar,
                        Localizator.ViewAllCars,
                        Localizator.ClearConsole,
                        Localizator.Exit
                    ] ) );

            switch ( choice )
            {
                case var _ when choice == Localizator.CreateCar:
                    CreateCar();
                    break;

                case var _ when choice == Localizator.ViewAllCars:
                    ShowCreatedCars();
                    break;

                case var _ when choice == Localizator.Exit:
                    _isExit = true;
                    return;

                case var _ when choice == Localizator.ClearConsole:
                    AnsiConsole.Clear();
                    break;
                default:
                    continue;
            }
        }
    }

    private void ShowCreatedCars()
    {
        if ( !_createdCars.Any() )
        {
            AnsiConsole.MarkupLine( $"[yellow]{Localizator.NoCarsAvailable}.[/]" );
            return;
        }

        foreach ( ICar car in _createdCars )
        {
            car.DisplayConfiguration();
            AnsiConsole.WriteLine();
        }
    }

    private void CreateCar()
    {
        try
        {
            string[] models = Enum.GetValues( typeof( CarModels ) ).Cast<CarModels>().Select( m => m.GetLocalizedName() ).ToArray();
            string[] colors = Enum.GetValues( typeof( CarColors ) ).Cast<CarColors>().Select( c => c.GetLocalizedName() ).ToArray();
            string[] wheelDrives = Enum.GetValues( typeof( WheelDrives ) ).Cast<WheelDrives>().Select( d => d.GetLocalizedName() ).ToArray();
            string[] wheelPositions = Enum.GetValues( typeof( WheelPositions ) ).Cast<WheelPositions>().Select( p => p.GetLocalizedName() ).ToArray();

            string modelName = GetSelection( models, Localizator.SelectModelPrompt );
            string colorName = GetSelection( colors, Localizator.SelectColorPrompt );
            string wheelDriveName = GetSelection( wheelDrives, Localizator.SelectWheelDrivePrompt );
            string wheelPositionName = GetSelection( wheelPositions, Localizator.SelectWheelPositionPrompt );

            ITransmission transmission = SelectTransmission();
            ICarEngine engine = SelectEngine();
            IBodyType bodyType = SelectBodyType();

            ICar car = CarFactory.CreateCar( modelName, bodyType, engine, transmission, colorName, wheelPositionName, wheelDriveName );
            _createdCars.Add( car );

            AnsiConsole.MarkupLine( $"[green]{Localizator.CarSuccessfullyCreated}![/]" );
            car.DisplayConfiguration();
        }
        catch ( Exception ex )
        {
            AnsiConsole.MarkupLine( $"[red]{Localizator.Error}: {ex.Message}[/]" );
        }
    }

    private static IBodyType SelectBodyType()
    {
        SelectionPrompt<IBodyType> selection = new SelectionPrompt<IBodyType>()
            .Title( $"[green]{Localizator.SelectBody}:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.Name}" )
            .AddChoices( CarData.BodyTypes );

        return AnsiConsole.Prompt( selection );
    }

    private static ICarEngine SelectEngine()
    {
        SelectionPrompt<ICarEngine> selection = new SelectionPrompt<ICarEngine>()
            .Title( $"[green]{Localizator.SelectEngine}:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.Name} ({t.HorsePower} {Localizator.HorsePowerTitle}.)" )
            .AddChoices( CarData.CarEngines );

        return AnsiConsole.Prompt( selection );
    }

    private static ITransmission SelectTransmission()
    {
        SelectionPrompt<ITransmission> selection = new SelectionPrompt<ITransmission>()
            .Title( $"[green]{Localizator.SelectTransmission}:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.GetName()} ({Localizator.GearsLabel}: {t.GetGearsAmount()})" )
            .AddChoices( CarData.Transmissions );

        return AnsiConsole.Prompt( selection );
    }

    private static string GetSelection( string[] choices, string title )
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title( title )
                .PageSize( 5 )
                .AddChoices( choices ) );
    }
}