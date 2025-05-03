using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public sealed class RoomServiceRepository : IRoomServiceRepository
{
    private static List<RoomService> _roomServices = [];

    public void Add( RoomService roomService )
    {
        _roomServices.Add( roomService );
    }

    public void DeleteById( Guid id )
    {
        RoomService roomService = GetById( id );

        _roomServices.Remove( roomService );
    }

    public List<RoomService> GetAll()
    {
        return _roomServices;
    }

    public RoomService GetById( Guid id )
    {
        RoomService? roomService = _roomServices.FirstOrDefault( p => p.Id == id );

        if ( roomService is null )
        {
            throw new InvalidOperationException( $"Room service with id {id} does not exist" );
        }

        return roomService;
    }

    public void Update( RoomService roomService )
    {
        RoomService existingRoomService = GetById( roomService.Id );

        existingRoomService.Name = roomService.Name;
    }
}