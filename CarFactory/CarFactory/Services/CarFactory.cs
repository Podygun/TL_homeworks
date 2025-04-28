using CarFactory.Domain;

namespace CarFactory.Services;

internal static class CarFactory
{
    public static ICar CreateCar( string model, string bodyType, string engine,
        string transmission, string color, string wheelPosition, int gearCount )
    {
        return new Car( model, bodyType, engine, transmission, color, wheelPosition, gearCount );

    }
}
