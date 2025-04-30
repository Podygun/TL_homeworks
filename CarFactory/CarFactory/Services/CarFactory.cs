using CarFactory.Domain;
using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;

namespace CarFactory.Services;

internal static class CarFactory
{
    public static ICar CreateCar( string model, IBodyType bodyType, ICarEngine engine,
        ITransmission transmission, string color, string? wheelPosition, string wheelDrive )
    {
        return new Car( model, bodyType, engine, transmission, color, wheelPosition, wheelDrive );
    }
}
