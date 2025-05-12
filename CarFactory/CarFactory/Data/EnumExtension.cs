using static CarFactory.Data.CarData;

namespace CarFactory.Data;

public static class EnumExtensions
{
    public static string GetLocalizedName( this CarModels model )
    {
        return Localizator.Get( model.ToString() );
    }

    public static string GetLocalizedName( this CarColors color )
    {
        return Localizator.Get( color.ToString() );
    }

    public static string GetLocalizedName( this WheelDrives wheelDrive )
    {
        return Localizator.Get( wheelDrive.ToString() );
    }

    public static string GetLocalizedName( this WheelPositions wheelPosition )
    {
        return Localizator.Get( wheelPosition.ToString() );
    }
}