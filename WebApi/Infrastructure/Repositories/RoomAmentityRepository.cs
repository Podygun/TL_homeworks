using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class RoomAmentityRepository : IRoomAmentityRepository
{
    public void Add( RoomAmentity roomAmentity )
    {
        WebApiDataStorage.RoomAmentities.Add( roomAmentity );
    }

    public void DeleteById( Guid id )
    {
        RoomAmentity roomAmentity = GetById( id );

        WebApiDataStorage.RoomAmentities.Remove( roomAmentity );
    }

    public List<RoomAmentity> GetAll()
    {
        return WebApiDataStorage.RoomAmentities;
    }

    public RoomAmentity GetById( Guid id )
    {
        RoomAmentity? roomAmentity = WebApiDataStorage.RoomAmentities.FirstOrDefault( p => p.Id == id );

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
