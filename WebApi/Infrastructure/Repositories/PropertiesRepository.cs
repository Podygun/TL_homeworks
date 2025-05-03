using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public sealed class PropertiesRepository : IPropertiesRepository
{
    private static List<Property> _properties = [];

    public void Add( Property property )
    {
        _properties.Add( property );
    }

    public void DeleteById( Guid id )
    {
        Property property = GetById( id );

        _properties.Remove( property );
    }

    public Property GetById( Guid id )
    {
        Property? property = _properties.FirstOrDefault( p => p.Id == id );

        if ( property is null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        return property;
    }

    public List<Property> GetAll()
    {
        return _properties;
    }

    public void Update( Property property )
    {
        Property existingProperty = GetById( property.Id );

        existingProperty.Name = property.Name;
    }
}