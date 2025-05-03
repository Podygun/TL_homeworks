using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class PropertiesRepository : IPropertiesRepository
{
    public void Add( Property property )
    {
        WebApiDataStorage.Properties.Add( property );
    }

    public void DeleteById( Guid id )
    {
        Property property = GetById( id );

        WebApiDataStorage.Properties.Remove( property );
    }

    public Property GetById( Guid id )
    {
        Property? property = WebApiDataStorage.Properties.FirstOrDefault( p => p.Id == id );

        if ( property is null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        return property;
    }

    public List<Property> GetAll()
    {
        return WebApiDataStorage.Properties;
    }

    public void Update( Property property )
    {
        Property existingProperty = GetById( property.Id );

        existingProperty.Name = property.Name;
    }
}