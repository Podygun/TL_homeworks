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

    public static readonly string[] Models = [ "Toyota", "BMW", "Audi" ];

    public static readonly string[] Colors = [ "Красный", "Синий", "Черный", "Белый" ];

    public static readonly string[] WheelDrive = [ "Передний", "Задний", "Полный" ];

    public static readonly string[] WheePosition = [ "Левый", "Правый" ];
}