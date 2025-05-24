using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RoomServicesRepository : IRoomServicesRepository
{
    private readonly ApplicationDbContext _context;

    public RoomServicesRepository( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<List<RoomService>> GetAllAsync()
    {
        return await _context.RoomServices.ToListAsync();
    }

    public async Task<RoomService> GetByIdAsync( int id )
    {
        RoomService? roomService = await _context.RoomServices.FindAsync( id );

        if ( roomService == null )
        {
            throw new InvalidOperationException( $"Room Service with id {id} does not exist" );
        }

        return roomService;
    }

    public async Task AddAsync( RoomService roomService )
    {
        await _context.RoomServices.AddAsync( roomService );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( RoomService roomService )
    {
        RoomService? existingRoomService = await GetByIdAsync( roomService.Id );

        if ( existingRoomService == null )
        {
            throw new InvalidOperationException( $"Room Service with id {roomService.Id} does not exist" );
        }

        existingRoomService.Name = roomService.Name;
        existingRoomService.RoomTypes = roomService.RoomTypes;

        _context.RoomServices.Update( existingRoomService );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync( int id )
    {
        RoomService? roomService = await GetByIdAsync( id );

        if ( roomService == null )
        {
            throw new InvalidOperationException( $"Property with id {roomService.Id} does not exist" );
        }

        _context.RoomServices.Remove( roomService );
        await _context.SaveChangesAsync();
    }
}