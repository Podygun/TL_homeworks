using Application.Mappers;
using Domain.Entities;
using PropertiesApi.Dtos.RoomTypes;

namespace PropertiesApi.Mappers;

internal static class RoomTypesMapper
{
    internal static List<RoomTypeDto> ToDto( this List<RoomType> roomTypes )
    {
        return roomTypes.Select( rt => new RoomTypeDto()
        {
            Id = rt.Id,
            PropertyId = rt.PropertyId,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount,

            RoomServices = rt.RoomServices.Select( rs =>
            {
                rs.RoomTypes = null;
                return rs;
            } ).ToList(),

            RoomAmentities = rt.RoomAmentities.Select( ra =>
            {
                ra.RoomTypes = null;
                return ra;
            } ).ToList()
        } ).ToList();
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

    internal static RoomTypeDto ToDto( this RoomType rt )
    {
        return new RoomTypeDto()
        {
            Id = rt.Id,
            PropertyId = rt.PropertyId,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount,

            RoomServices = rt.RoomServices.Select( rs =>
            {
                rs.RoomTypes = null;
                return rs;

            } ).ToList(),
            RoomAmentities = rt.RoomAmentities.Select( ra =>
            {
                ra.RoomTypes = null;
                return ra;
            } ).ToList()
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
