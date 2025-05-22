using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RoomTypeRepository : IRoomTypesRepository
{
    private readonly ApplicationDbContext _context;

    public RoomTypeRepository( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<RoomType> GetByIdAsync( Guid id )
    {
        RoomType? roomType = await _context.RoomTypes
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.RoomAmentities )
            .FirstOrDefaultAsync( rt => rt.Id == id );

        if ( roomType == null )
        {
            throw new InvalidOperationException( $"Room Type with id {id} does not exist" );
        }

        return roomType;
    }

    public async Task<List<RoomType>> GetAllAsync()
    {
        return await _context.RoomTypes
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.RoomAmentities )
            .ToListAsync();
    }

    public async Task<List<RoomType>> GetByPropertyId( Guid propertyId )
    {
        return await _context.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .Include( rt => rt.RoomServices )
            .Include( rt => rt.RoomAmentities )
            .ToListAsync();
    }

    public async Task AddAsync( RoomType roomType )
    {
        await _context.RoomTypes.AddAsync( roomType );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( RoomType roomType )
    {
        RoomType? existingRoomType = await GetByIdAsync( roomType.Id );

        if ( existingRoomType == null )
        {
            throw new InvalidOperationException( $"Room Type with id {roomType.Id} does not exist" );
        }

        existingRoomType.Currency = roomType.Currency;
        existingRoomType.DailyPrice = roomType.DailyPrice;
        existingRoomType.MinPersonCount = roomType.MinPersonCount;
        existingRoomType.MaxPersonCount = roomType.MaxPersonCount;

        existingRoomType.RoomServices.Clear();
        foreach ( RoomService service in roomType.RoomServices )
        {
            existingRoomType.RoomServices.Add( service );
        }

        existingRoomType.RoomAmentities.Clear();

        foreach ( RoomAmentity amentity in roomType.RoomAmentities )
        {
            existingRoomType.RoomAmentities.Add( amentity );
        }

        _context.RoomTypes.Update( existingRoomType );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync( Guid id )
    {
        RoomType? roomType = await GetByIdAsync( id );

        if ( roomType != null )
        {
            _context.RoomTypes.Remove( roomType );
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }
    }


}
