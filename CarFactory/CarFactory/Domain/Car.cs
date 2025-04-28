using Spectre.Console;

namespace CarFactory.Domain;

public sealed class Car : ICar
{
    public string Model { get; }
    public string BodyType { get; }
    public string Engine { get; }
    public string Transmission { get; }
    public string Color { get; }
    public string WheelPosition { get; }
    public int MaxSpeed { get; }
    public int GearCount { get; }

    public Car( string model, string bodyType, string engine, string transmission, string color, string wheelPosition, int gearCount )
    {
        Model = model;
        BodyType = bodyType;
        Engine = engine;
        Transmission = transmission;
        Color = color;
        WheelPosition = wheelPosition;
        GearCount = gearCount;

        MaxSpeed = CalculateMaxSpeed( Engine, GearCount );
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
