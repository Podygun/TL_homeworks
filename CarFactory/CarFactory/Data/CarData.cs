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

    public static readonly IReadOnlyList<string> Models =
        new List<string> { "Toyota", "BMW", "Audi" }.AsReadOnly();

    public static readonly IReadOnlyList<string> Colors =
        new List<string> { "Красный", "Синий", "Черный", "Белый" }.AsReadOnly();

    public static readonly IReadOnlyList<string> WheelDrive =
        new List<string> { "Передний", "Задний", "Полный" }.AsReadOnly();

    public static readonly IReadOnlyList<string> WheelPosition =
        new List<string> { "Левый", "Правый" }.AsReadOnly();
}