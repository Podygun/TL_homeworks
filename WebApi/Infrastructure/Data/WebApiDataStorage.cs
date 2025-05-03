using Domain.Entities;

namespace Infrastructure.Data;

/// <summary>
/// Данный класс представляет коллекцию данных в виде списков, как база данных.
/// </summary>
internal sealed class WebApiDataStorage
{
    //internal static List<Property> Properties = [ ];

    internal static List<Property> Properties = [ new Property( "Test property", "Address value", "Some city", "Some country", 100d, 200d ) ];

    internal static List<RoomAmentity> RoomAmentities = [];

    internal static List<RoomService> RoomServices = [];

    internal static List<RoomType> RoomTypes = [];
}
