using CarFactory.Domain.BodyTypes;
using CarFactory.Domain.Engines;
using CarFactory.Domain.Transmissions;

namespace CarFactory.Data;

public static class CarData
{
    internal static readonly IReadOnlyList<ITransmission> Transmissions =
        [ new AutomaticTransmission(), new ManualTransmission(), new VariatorTransmission() ];

    internal static readonly IReadOnlyList<IBodyType> BodyTypes =
        [ new HatchbackBodyType(), new CoupeBodyType(), new SedanBodyType() ];

    internal static readonly IReadOnlyList<ICarEngine> CarEngines =
        [ new DieselCarEngine(), new PetrolCarEngine(), new ElectricCarEngine() ];

    public enum CarModels
    {
        Toyota = 0,
        BMW = 1,
        Audi = 2
    }

    public enum CarColors
    {
        Red = 0,
        Blue = 1,
        Black = 2,
        White = 3
    }

    public enum WheelDrives
    {
        Front = 0,
        Rear = 1,
        Full = 2
    }

    public enum WheelPositions
    {
        Left = 0,
        Right = 1
    }
}