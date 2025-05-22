using Domain.Entities;
using PropertiesApi.Dtos.Properties;

namespace Application.Mappers;

internal static class PropertiesMapper
{
    internal static List<PropertyDto> ToDto( this List<Property> props )
    {
        return props.Select( p => new PropertyDto()
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            Country = p.Country,
            City = p.City,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            RoomTypes = p.RoomTypes.Select( rt =>
            {
                rt.Property = null;
                return rt;
            } ).ToList(),
        } ).ToList();
    }

    internal static List<Property> ToDomain( this List<PropertyDto> props )
    {
        return props.Select( p => new Property()
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            Country = p.Country,
            City = p.City,
            Latitude = p.Latitude,
            Longitude = p.Longitude
        } ).ToList();
    }

    internal static Property ToDomain( this PropertyDto p )
    {
        return new Property()
        {
            Name = p.Name,
            Country = p.Country,
            City = p.City,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude
        };
    }

    internal static PropertyDto ToDto( this Property p )
    {
        return new PropertyDto()
        {
            Id = p.Id,
            Name = p.Name,
            Country = p.Country,
            City = p.City,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            RoomTypes = p.RoomTypes.Select( rt =>
            {
                rt.Property = null;
                return rt;
            } ).ToList(),
        };
    }

    internal static Property ToDomain( this CreatePropertyDto p )
    {
        return new Property()
        {
            Name = p.Name,
            Country = p.Country,
            City = p.City,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude
        };
    }
}
