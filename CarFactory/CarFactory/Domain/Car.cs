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

        Table table = new Table();

        table.Title( $"[bold yellow]{Localizator.ConfigurationTitle} {Model}[/]" );
        table.Border( TableBorder.Rounded );
        table.AddColumn( new TableColumn( $"[bold]{Localizator.DescriptionColumn}[/]" ).Centered() );
        table.AddColumn( new TableColumn( $"[bold]{Localizator.PropertiesColumn}[/]" ).Centered() );

        table.AddRow( $"[blue]{Localizator.ModelLabel}[/]", Model ?? Localizator.NoName );
        table.AddRow( $"[blue]{Localizator.BodyLabel}[/]", BodyType?.Name ?? Localizator.NoName );
        table.AddRow( $"[blue]{Localizator.ColorLabel}[/]", Color ?? Localizator.NoName );
        table.AddRow( $"[blue]{Localizator.EngineLabel}[/]", $"{CarEngine?.Name ?? Localizator.NoName} ([green]{CarEngine?.HorsePower ?? 0} {Localizator.HorsePowerTitle}[/])" );
        table.AddRow( $"[blue]{Localizator.TransmissionLabel}[/]", $"{Transmission?.GetName() ?? Localizator.NoName} ([green]{Localizator.GearsLabel}: {Transmission?.GetGearsAmount() ?? 0}[/])" );
        table.AddRow( $"[blue]{Localizator.WheelDriveLabel}[/]", WheelDrive ?? Localizator.NoName );
        table.AddRow( $"[blue]{Localizator.WheelPositionLabel}[/]", WheelPosition ?? Localizator.NoName );
        table.AddRow( $"[blue]{Localizator.MaxSpeedLabel}[/]", $"[bold]{MaxSpeed} {Localizator.SpeedValue}[/]" );

        AnsiConsole.Write( table );
    }
}
