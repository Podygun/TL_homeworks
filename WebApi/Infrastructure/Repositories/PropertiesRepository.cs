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

    public async Task<Property> GetByIdAsync( Guid id )
    {
        Property? property = await _context.Properties.FindAsync( id );

        if ( property is null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        return property;
    }

    public async Task AddAsync( Property property )
    {
        await _context.Properties.AddAsync( property );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( Property property )
    {
        Property? existingProperty = await GetByIdAsync( property.Id );

        if ( existingProperty is null )
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

    public async Task DeleteByIdAsync( Guid id )
    {
        Property? property = await GetByIdAsync( id );

        if ( property is null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        _context.Properties.Remove( property );
        await _context.SaveChangesAsync();
    }

}