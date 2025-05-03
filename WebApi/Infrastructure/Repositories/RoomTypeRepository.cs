using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public sealed class RoomTypeRepository : IRoomTypeRepository
{
    private static List<RoomType> _roomTypes = [];

    public void Add( RoomType roomType )
    {
        _roomTypes.Add( roomType );
    }

    public void DeleteById( Guid id )
    {
        RoomType roomType = GetById( id );

        _roomTypes.Remove( roomType );
    }

    public List<RoomType> GetAll()
    {
        return _roomTypes;
    }

    public RoomType GetById( Guid id )
    {
        RoomType? roomType = _roomTypes.FirstOrDefault( p => p.Id == id );

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
