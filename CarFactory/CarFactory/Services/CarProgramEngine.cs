using CarFactory.Data;
using CarFactory.Domain;
using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;
using Spectre.Console;

namespace CarFactory.Services;

internal sealed class CarProgramEngine
{
    private readonly List<ICar> _createdCars = [];

    public void Run()
    {
        AnsiConsole
            .Write( new FigletText( "Car Factory" )
            .LeftJustified()
            .Color( Color.Red ) );

        while ( true )
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title( "Выберите действие:" )
                    .PageSize( 10 )
                    .AddChoices(
                    [
                        "Создать машину",
                        "Посмотреть все созданные машины",
                        "Выход"
                    ] ) );

            switch ( choice )
            {
                case "Создать машину":
                    CreateCar();
                    break;

                case "Посмотреть все созданные машины":
                    ShowCreatedCars();
                    break;

                case "Выход":
                    return;
            }
        }
    }

    private void ShowCreatedCars()
    {
        if ( _createdCars.Count == 0 )
        {
            AnsiConsole.MarkupLine( "[yellow]Машин пока нет.[/]" );
            return;
        }

        Console.Clear();

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
            string model = GetSelection( "Выберите модель:", CarData.Models );
            string color = GetSelection( "Выберите цвет:", CarData.Colors );
            string wheelDrive = GetSelection( "Выберите привод:", CarData.WheelDrive );
            string wheelPosition = GetSelection( "Выберите сторону руля:", CarData.WheelPosition );

            ITransmission transmission = SelectTransmission();
            ICarEngine engine = SelectEngine();
            IBodyType bodyType = SelectBodyType();

            ICar car = CarFactory.CreateCar( model, bodyType, engine, transmission, color, wheelPosition, wheelDrive );
            _createdCars.Add( car );

            AnsiConsole.MarkupLine( "[green]Машина успешно создана![/]" );
            car.DisplayConfiguration();
        }
        catch ( Exception ex )
        {
            AnsiConsole.MarkupLine( $"[red]Ошибка: {ex.Message}[/]" );
        }
    }

    private static IBodyType SelectBodyType()
    {
        SelectionPrompt<IBodyType> selection = new SelectionPrompt<IBodyType>()
            .Title( "[green]Выберите кузов:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.Name}" )
            .AddChoices( CarData.BodyTypes );

        return AnsiConsole.Prompt( selection );
    }

    private static ICarEngine SelectEngine()
    {
        SelectionPrompt<ICarEngine> selection = new SelectionPrompt<ICarEngine>()
            .Title( "[green]Выберите двигатель:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.Name} ({t.HorsePower} л.с.)" )
            .AddChoices( CarData.CarEngines );

        return AnsiConsole.Prompt( selection );
    }

    private static ITransmission SelectTransmission()
    {
        SelectionPrompt<ITransmission> selection = new SelectionPrompt<ITransmission>()
            .Title( "[green]Выберите тип трансмиссии:[/]" )
            .PageSize( 5 )
            .UseConverter( t => $"{t.GetName()} (Кол-во передач: {t.GetGearsAmount()})" )
            .AddChoices( CarData.Transmissions );

        return AnsiConsole.Prompt( selection );
    }

    private static string GetSelection( string title, IReadOnlyList<string> options )
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title( $"[green]{title}[/]" )
                .PageSize( 10 )
                .AddChoices( options ) );
    }
}
