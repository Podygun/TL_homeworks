using Domain.Entities;
using ReservationApi.Dtos.Properties;
using ReservationApi.Dtos.RoomAmentities;
using ReservationApi.Dtos.RoomServices;
using ReservationApi.Dtos.RoomTypes;

namespace ReservationApi.Mappers;

internal static class PropertiesMapper
{
    internal static List<FoundPropertyDto> ToDto( this List<Property> props )
    {
        return props.Select( p => new FoundPropertyDto()
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            Country = p.Country,
            City = p.City,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            RoomTypes = p.RoomTypes?.Select( rt => new RoomTypeDto()
            {
                Id = rt.Id,
                DailyPrice = rt.DailyPrice,
                Currency = rt.Currency,
                MinPersonCount = rt.MinPersonCount,
                MaxPersonCount = rt.MaxPersonCount,
                RoomAmentities = rt.RoomAmentities?.Select( ra => new RoomAmentityDto() { Id = ra.Id, Name = ra.Name } ).ToList(),
                RoomServices = rt.RoomServices?.Select( rc => new RoomServiceDto() { Id = rc.Id, Name = rc.Name } ).ToList(),
            } ).ToList() ?? new List<RoomTypeDto>()
        } ).ToList();
    }
}
