using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;
using Spectre.Console;

namespace CarFactory.Domain;

internal sealed class Car : ICar
{
    public string Model { get; }
    public IBodyType BodyType { get; }
    public ICarEngine CarEngine { get; }
    public ITransmission Transmission { get; }
    public string Color { get; }
    public string WheelPosition { get; }
    public int MaxSpeed => CalculateMaxSpeed();
    public int GearCount => Transmission.GetGearsAmount();

    public Car( string model, IBodyType bodyType, ICarEngine engine,
              ITransmission transmission, string color, string wheelPosition )
    {
        Model = model;
        BodyType = bodyType;
        CarEngine = engine;
        Transmission = transmission;
        Color = color;
        WheelPosition = wheelPosition ?? "Left"; // По умолчанию леворульный
    }

    private static int CalculateMaxSpeed( string engine, int gearCount )
    {
        int maxSpeed = 0;

        if ( engine.ToLower().Contains( "электр" ) )
        {
            maxSpeed = 300;
        }
        else if ( engine.ToLower().Contains( "дизель" ) )
        {
            maxSpeed = 50 * gearCount;
        }
        else if ( engine.ToLower().Contains( "бензин" ) )
        {
            maxSpeed = 70 * gearCount;
        }
        else
        {
            maxSpeed = 200;
        }

        return maxSpeed;
    }

    public void DisplayConfiguration()
    {
        var table = new Table();
        table.AddColumn( "Parameter" );
        table.AddColumn( "Value" );

        table.AddRow( "Модель", Model );
        table.AddRow( "Кузов", BodyType );
        table.AddRow( "Двигатель", Engine );
        table.AddRow( "Тип КПП", Transmission );
        table.AddRow( "Кол-во передач", GearCount.ToString() );
        table.AddRow( "Цвет", Color );
        table.AddRow( "Привод", WheelPosition );
        table.AddRow( "Макс. ск.", $"{MaxSpeed} km/h" );

        AnsiConsole.Write( table );
    }
}
