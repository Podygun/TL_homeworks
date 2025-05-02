using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;
using Spectre.Console;

namespace CarFactory.Domain;

internal sealed class Car : ICar
{
    public string Model { get; }
    public string Color { get; }
    public string WheelPosition { get; }
    public string WheelDrive { get; }
    public int MaxSpeed => CalculateMaxSpeed();
    public int GearCount => Transmission.GetGearsAmount();

    public IBodyType BodyType { get; }
    public ICarEngine CarEngine { get; }
    public ITransmission Transmission { get; }

    private const int MinGearsAmountForCalculateMaxSpeed = 5;

    public Car( string model, IBodyType bodyType, ICarEngine engine,
        ITransmission transmission, string color, string wheelPosition, string wheelDrive )
    {
        Model = model;
        BodyType = bodyType;
        CarEngine = engine;
        Transmission = transmission;
        Color = color;
        WheelPosition = wheelPosition;
        WheelDrive = wheelDrive;
    }

    private int CalculateMaxSpeed()
    {
        int gearsRatioAspect = GearCount <= MinGearsAmountForCalculateMaxSpeed ?
            MinGearsAmountForCalculateMaxSpeed : GearCount;

        int maxSpeed = CarEngine.HorsePower / 2 + gearsRatioAspect * 10;

        return maxSpeed;
    }

    public void DisplayConfiguration()
    {
        AnsiConsole.WriteLine();

        Table table = new Table()
            .Title( $"[bold yellow]Конфигурация {Model}[/]" )
            .Border( TableBorder.Rounded )
            .AddColumn( new TableColumn( "[bold]Описание[/]" ).Centered() )
            .AddColumn( new TableColumn( "[bold]Свойства[/]" ).Centered() );

        table.AddRow( "[blue]Модель[/]", Model );
        table.AddRow( "[blue]Кузов[/]", $"{BodyType.Name} " );
        table.AddRow( "[blue]Цвет[/]", $"{Color}" );
        table.AddRow( "[blue]Двигатель[/]", $"{CarEngine.Name} ([green]{CarEngine.HorsePower} л.с.[/])" );
        table.AddRow( "[blue]Трансмиссия[/]", $"{Transmission.GetName()} ([green]Передач: {Transmission.GetGearsAmount()}[/])" );
        table.AddRow( "[blue]Привод[/]", $"{WheelDrive}" );
        table.AddRow( "[blue]Руль[/]", $"{WheelPosition}" );
        table.AddRow( "[blue]Макс. скорость[/]", $"[bold]{MaxSpeed} км/ч[/]" );

        AnsiConsole.Write( table );
    }
}
