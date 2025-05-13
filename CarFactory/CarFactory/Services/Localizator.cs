using System.Globalization;
using System.Resources;

namespace CarFactory.Services;

public sealed class Localizator
{
    private static ResourceManager? _resourceManager;
    private static CultureInfo? _culture;

    static Localizator()
    {
        _resourceManager = new ResourceManager( "CarFactory.Resources.Strings", typeof( Localizator ).Assembly );
        SetCulture( "en-US" );
    }

    public static void SetCulture( string cultureCode )
    {
        _culture = new CultureInfo( cultureCode );
    }

    public static string Get( string key )
    {
        return _resourceManager?.GetString( key, _culture ) ?? key;
    }

    // Форматируемые строки (с параметрами)
    public static string ConfigurationTitle => Get( "ConfigurationTitle" );
    public static string TransmissionWithGears( string name, int gears ) => $"{name} ([green]Gears: {gears}[/])";

    // Статические строки
    public static string DescriptionColumn => Get( "DescriptionColumn" );
    public static string PropertiesColumn => Get( "PropertiesColumn" );
    public static string ModelLabel => Get( "ModelLabel" );
    public static string BodyLabel => Get( "BodyLabel" );
    public static string ColorLabel => Get( "ColorLabel" );
    public static string EngineLabel => Get( "EngineLabel" );
    public static string GearsLabel => Get( "GearsLabel" );
    public static string TransmissionLabel => Get( "TransmissionLabel" );
    public static string WheelDriveLabel => Get( "WheelDriveLabel" );
    public static string WheelPositionLabel => Get( "WheelPositionLabel" );
    public static string MaxSpeedLabel => Get( "MaxSpeedLabel" );
    public static string ErrorPrefix => Get( "ErrorPrefix" );
    public static string NoCarsMessage => Get( "NoCarsMessage" );
    public static string SelectAction => Get( "SelectAction" );
    public static string SelectModelPrompt => Get( "SelectModelPrompt" );
    public static string SelectColorPrompt => Get( "SelectColorPrompt" );
    public static string SelectWheelDrivePrompt => Get( "SelectWheelDrivePrompt" );
    public static string SelectWheelPositionPrompt => Get( "SelectWheelPositionPrompt" );
    public static string CarCreatedSuccess => Get( "CarCreatedSuccess" );
    public static string SpeedValue => Get( "SpeedValue" );
    public static string CreateCar => Get( "CreateCar" );
    public static string ViewAllCars => Get( "ViewAllCars" );
    public static string ClearConsole => Get( "ClearConsole" );
    public static string Exit => Get( "Exit" );
    public static string NoCarsAvailable => Get( "NoCarsAvailable" );
    public static string SelectBody => Get( "SelectBody" );
    public static string SelectEngine => Get( "SelectEngine" );
    public static string SelectTransmission => Get( "SelectTransmission" );
    public static string CarSuccessfullyCreated => Get( "CarSuccessfullyCreated" );
    public static string Error => Get( "Error" );
    public static string HorsePowerTitle => Get( "HorsePowerTitle" );
    public static string NoName => Get( "NoName" );

    // Типы кузовов
    public static string CoupeBodyType => Get( "CoupeBodyType" );
    public static string HatchbackBodyType => Get( "HatchbackBodyType" );
    public static string SedanBodyType => Get( "SedanBodyType" );

    // Типы двигателей
    public static string DieselEngine => Get( "DieselEngine" );
    public static string ElectricEngine => Get( "ElectricEngine" );
    public static string PetrolEngine => Get( "PetrolEngine" );

    // Типы трансмиссий
    public static string AutomaticTransmission => Get( "AutomaticTransmission" );
    public static string ManualTransmission => Get( "ManualTransmission" );
    public static string VariatorTransmission => Get( "VariatorTransmission" );

}
