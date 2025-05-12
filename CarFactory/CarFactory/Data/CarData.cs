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
        Toyota,
        BMW,
        Audi
    }

    public enum CarColors
    {
        Red,
        Blue,
        Black,
        White
    }

    public enum WheelDrives
    {
        Front,
        Rear,
        Full
    }

    public enum WheelPositions
    {
        Left,
        Right
    }
}