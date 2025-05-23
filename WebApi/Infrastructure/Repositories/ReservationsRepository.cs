using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<Reservation> GetByIdAsync( int id )
    {
        return await _context.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .FirstOrDefaultAsync( r => r.Id == id );
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .ToListAsync();
    }

    public async Task AddAsync( Reservation entity )
    {
        await _context.Reservations.AddAsync( entity );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( Reservation entity )
    {
        _context.Reservations.Update( entity );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync( int id )
    {
        var reservation = await GetByIdAsync( id );
        if ( reservation != null )
        {
            _context.Reservations.Remove( reservation );
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Reservation>> GetByPropertyIdAsync( int propertyId )
    {
        return await _context.Reservations
            .Where( r => r.PropertyId == propertyId )
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByDatesAsync( DateTime from, DateTime to )
    {
        return await _context.Reservations
            .Where( r => r.ArrivalDate <= to && r.DepartureDate >= from )
            .ToListAsync();
    }

    public async Task<bool> IsRoomAvailableAsync( int roomTypeId, DateTime arrivalDate, DateTime departureDate )
    {
        return !await _context.Reservations
            .AnyAsync( r => r.RoomTypeId == roomTypeId &&
                          r.ArrivalDate < departureDate &&
                          r.DepartureDate > arrivalDate );
    }
}
