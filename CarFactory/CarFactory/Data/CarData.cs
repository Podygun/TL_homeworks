namespace CarFactory.Data;

public static class CarData
{
    // Изменения данных значений могут привести к непредсказуемой работе программы

    public static readonly string[] Models = [ "Toyota", "BMW", "Audi" ];
    public static readonly string[] Colors = [ "Красный", "Синий", "Черный", "Белый" ];
    public static readonly string[] EngineTypes = [ "Бензиновый 2.0L", "Дизель 2.5L", "Гибридный 1.8L", "Электрический" ];
    public static readonly string[] TransmissionTypes = [ "АКПП", "МКПП", "CVT" ];
    public static readonly string[] WheelDrive = [ "Передний", "Задний", "Полный" ];
    public static readonly string[] BodyTypes = [ "Седан", "Универсал", "Хэтчбэк", "Купе" ];

    public static readonly Dictionary<string, string> Modelss = new() { { "Toyota"} }
}