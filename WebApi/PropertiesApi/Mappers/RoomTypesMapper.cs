using Domain.Entities;
using Microsoft.AspNetCore.DataProtection.Repositories;
using PropertiesApi.Dtos.RoomAmentities;
using PropertiesApi.Dtos.RoomServices;
using PropertiesApi.Dtos.RoomTypes;

namespace PropertiesApi.Mappers;

internal static class RoomTypesMapper
{
    internal static List<RoomTypeDto> ToDto( this List<RoomType> roomTypes )
    {
        return roomTypes.Select( rt => rt.ToDto() ).ToList();
    }

    internal static List<RoomType> ToDomain( this List<RoomTypeDto> roomTypes )
    {
        return roomTypes.Select( rt => new RoomType()
        {
            Id = rt.Id,
            PropertyId = rt.PropertyId,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount
        } ).ToList();
    }

    internal static RoomType ToDomain( this RoomTypeDto rt )
    {
        return new RoomType()
        {
            PropertyId = rt.PropertyId,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount
        };
    }

    internal static RoomTypeDto ToDto( this RoomType roomType )
    {
        return new RoomTypeDto()
        {
            Id = roomType.Id,
            PropertyId = roomType.PropertyId,
            DailyPrice = roomType.DailyPrice,
            Currency = roomType.Currency,
            MinPersonCount = roomType.MinPersonCount,
            MaxPersonCount = roomType.MaxPersonCount,
            RoomServicesDto = roomType.RoomServices?.ToDto() ?? new List<RoomServiceDto>(),
            RoomAmentitiesDto = roomType.RoomAmentities?.ToDto() ?? new List<RoomAmentityDto>()
        };
    }

    internal static RoomType ToDomain( this CreateRoomTypeDto rt )
    {
        return new RoomType()
        {
            PropertyId = rt.PropertyId,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount
        };
    }
}
