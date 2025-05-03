using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class RoomServiceRepository : IRoomServiceRepository
{
    public void Add( RoomService roomService )
    {
        WebApiDataStorage.RoomServices.Add( roomService );
    }

    public void DeleteById( Guid id )
    {
        RoomService roomService = GetById( id );

        WebApiDataStorage.RoomServices.Remove( roomService );
    }

    public List<RoomService> GetAll()
    {
        return WebApiDataStorage.RoomServices;
    }

    public RoomService GetById( Guid id )
    {
        RoomService? roomService = WebApiDataStorage.RoomServices.FirstOrDefault( p => p.Id == id );

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