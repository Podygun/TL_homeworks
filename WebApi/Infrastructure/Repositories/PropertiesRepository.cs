using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class PropertiesRepository : IPropertiesRepository
{
    private readonly ApplicationDbContext _context;

    public PropertiesRepository( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync( int id )
    {
        return await _context.Properties.FindAsync( id );
    }

    public async Task AddAsync( Property property )
    {
        await _context.Properties.AddAsync( property );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( Property property )
    {
        Property? existingProperty = await GetByIdAsync( property.Id );

        if ( existingProperty == null )
        {
            throw new InvalidOperationException( $"Property with id {property.Id} does not exist" );
        }

        existingProperty.Name = property.Name;
        existingProperty.Country = property.Country;
        existingProperty.Address = property.Address;
        existingProperty.Longitude = property.Longitude;
        existingProperty.Latitude = property.Latitude;
        existingProperty.City = property.City;

        _context.Properties.Update( existingProperty );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync( int id )
    {
        Property? property = await GetByIdAsync( id );

        if ( property == null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        _context.Properties.Remove( property );
        await _context.SaveChangesAsync();
    }

    public async Task<List<Property>> GetPropertiesByFiltersAsync(
        string city,
        DateTime arrivalDate,
        DateTime departureDate,
        int guests,
        decimal? maxPrice
        )
    {
        return await _context.Properties

            .Include( p => p.RoomTypes )
                .ThenInclude( rt => rt.RoomServices )

            .Include( p => p.RoomTypes )
                .ThenInclude( rt => rt.RoomAmentities )

            .Where( p => p.City == city )

            .Select( p => new Property
            {
                Id = p.Id,
                Name = p.Name,
                City = p.City,
                Country = p.Country,
                Address = p.Address,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                RoomTypes = p.RoomTypes
                    .Where( rt =>
                        rt.MaxPersonCount >= guests &&
                        ( !maxPrice.HasValue || rt.DailyPrice <= maxPrice ) &&
                        rt.AmountRooms > 0 &&
                        !rt.Reservations.Any( r =>
                            arrivalDate < r.DepartureDateTime &&
                            departureDate > r.ArrivalDateTime
                        )
                    )
                    .Select( rt => new RoomType
                    {
                        Id = rt.Id,
                        PropertyId = rt.PropertyId,
                        DailyPrice = rt.DailyPrice,
                        Currency = rt.Currency,
                        MinPersonCount = rt.MinPersonCount,
                        MaxPersonCount = rt.MaxPersonCount,
                        AmountRooms = rt.AmountRooms,
                        RoomServices = rt.RoomServices.ToList(),
                        RoomAmentities = rt.RoomAmentities.ToList()
                    } )
                    .ToList()
            } )
            .Where( p => p.RoomTypes.Any() )
            .ToListAsync();
    }
}