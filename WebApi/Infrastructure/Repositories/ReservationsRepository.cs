using Domain.Entities;
using Domain.Repositories;
using Domain.Utilities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationsRepository( ApplicationDbContext context )
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

    public async Task AddAsync( Reservation entity )
    {
        await _context.Reservations.AddAsync( entity );
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

    public async Task<IEnumerable<Reservation>> GetFilteredReservationAsync( ReservationFilter filter )
    {
        IQueryable<Reservation> query = _context.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .AsQueryable();

        if ( filter.PropertyId.HasValue )
            query = query.Where( r => r.PropertyId == filter.PropertyId );

        if ( filter.RoomTypeId.HasValue )
            query = query.Where( r => r.RoomTypeId == filter.RoomTypeId );

        if ( filter.ArrivalDateFrom.HasValue )
            query = query.Where( r => r.ArrivalDateTime >= filter.ArrivalDateFrom.Value.ToDateTime( TimeOnly.MinValue ) );

        if ( filter.ArrivalDateTo.HasValue )
            query = query.Where( r => r.ArrivalDateTime <= filter.ArrivalDateTo.Value.ToDateTime( TimeOnly.MaxValue ) );

        if ( filter.DepartureDateFrom.HasValue )
            query = query.Where( r => r.ArrivalDateTime >= filter.DepartureDateFrom.Value.ToDateTime( TimeOnly.MinValue ) );

        if ( filter.DepartureDateTo.HasValue )
            query = query.Where( r => r.ArrivalDateTime <= filter.DepartureDateTo.Value.ToDateTime( TimeOnly.MaxValue ) );

        if ( !string.IsNullOrEmpty( filter.GuestName ) )
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );

        if ( !string.IsNullOrEmpty( filter.GuestPhone ) )
            query = query.Where( r => r.GuestPhoneNumber.Contains( filter.GuestPhone ) );

        if ( filter.MinTotal.HasValue )
            query = query.Where( r => r.Total >= filter.MinTotal );

        if ( filter.MaxTotal.HasValue )
            query = query.Where( r => r.Total <= filter.MaxTotal );

        return await query.ToListAsync();
    }
}
