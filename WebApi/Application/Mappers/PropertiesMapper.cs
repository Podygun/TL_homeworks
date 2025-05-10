using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public class PropertiesMapper : IMapper<Property, PropertyDto>
{
    public List<PropertyDto> Convert( List<Property> props )
    {
        return props.Select( p => new PropertyDto(
            p.Id, p.Name, p.Address, p.Country, p.City, p.Latitude, p.Longitude ) ).ToList();
    }

    public List<Property> Convert( List<PropertyDto> props )
    {
        return props.Select( p => new Property(
            p.Id, p.Name, p.Address, p.Country, p.City, p.Latitude, p.Longitude ) ).ToList();
    }

    public Property Convert( PropertyDto p )
    {
        return new Property( p.Name, p.Country, p.City, p.Address, p.Latitude, p.Longitude );
    }

    public PropertyDto Convert( Property p )
    {
        return new PropertyDto( p.Id, p.Name, p.Country, p.City, p.Address, p.Latitude, p.Longitude );
    }

}
