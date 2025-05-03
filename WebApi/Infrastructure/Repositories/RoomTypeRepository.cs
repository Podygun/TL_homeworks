using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class RoomTypeRepository : IRoomTypeRepository
{
    public void Add( RoomType roomType )
    {
        WebApiDataStorage.RoomTypes.Add( roomType );
    }

    public void DeleteById( Guid id )
    {
        RoomType roomType = GetById( id );

        WebApiDataStorage.RoomTypes.Remove( roomType );
    }

    public List<RoomType> GetAll()
    {
        return WebApiDataStorage.RoomTypes;
    }

    public RoomType GetById( Guid id )
    {
        RoomType? roomType = WebApiDataStorage.RoomTypes.FirstOrDefault( p => p.Id == id );

        if ( roomType is null )
        {
            throw new InvalidOperationException( $"Room type with id {id} does not exist" );
        }

        return roomType;
    }

    public void Update( RoomType roomType )
    {
        RoomType existingRoomType = GetById( roomType.Id );

        existingRoomType.PropertyId = roomType.PropertyId;
        existingRoomType.DailyPrice = roomType.DailyPrice;
        existingRoomType.Currency = roomType.Currency;
        existingRoomType.MinPersonCount = roomType.MinPersonCount;
        existingRoomType.MaxPersonCount = roomType.MaxPersonCount;
        existingRoomType.RoomServices = roomType.RoomServices;
        existingRoomType.RoomAmentities = roomType.RoomAmentities;
    }
}
