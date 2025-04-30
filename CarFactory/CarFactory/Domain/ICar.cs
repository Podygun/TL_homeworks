using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;

namespace CarFactory.Domain;

internal interface ICar
{
    string Model { get; }
    string Color { get; }
    string WheelPosition { get; }
    string WheelDrive { get; }
    int MaxSpeed { get; }

    IBodyType BodyType { get; }
    ICarEngine CarEngine { get; }
    ITransmission Transmission { get; }

    void DisplayConfiguration();
}