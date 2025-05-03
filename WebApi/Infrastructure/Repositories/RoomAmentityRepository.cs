using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public sealed class RoomAmentityRepository : IRoomAmentityRepository
{
    private static List<RoomAmentity> _roomAmentities = [];

    public void Add( RoomAmentity roomAmentity )
    {
        _roomAmentities.Add( roomAmentity );
    }

    public void DeleteById( Guid id )
    {
        RoomAmentity roomAmentity = GetById( id );

        _roomAmentities.Remove( roomAmentity );
    }

    public List<RoomAmentity> GetAll()
    {
        return _roomAmentities;
    }

    public RoomAmentity GetById( Guid id )
    {
        RoomAmentity? roomAmentity = _roomAmentities.FirstOrDefault( p => p.Id == id );

        if ( roomAmentity is null )
        {
            throw new InvalidOperationException( $"Room amentity with id {id} does not exist" );
        }

        return roomAmentity;
    }

    public void Update( RoomAmentity roomAmentity )
    {
        RoomAmentity existingRoomAmentity = GetById( roomAmentity.Id );

        existingRoomAmentity.Name = roomAmentity.Name;
    }
}
