using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;

namespace CarFactory.Domain;

internal interface ICar
{
    public string Model { get; }
    public IBodyType BodyType { get; }
    public ICarEngine CarEngine { get; }
    public ITransmission Transmission { get; }
    public string Color { get; }
    public string WheelPosition { get; }
    public int MaxSpeed { get; }

    public void DisplayConfiguration();
}
