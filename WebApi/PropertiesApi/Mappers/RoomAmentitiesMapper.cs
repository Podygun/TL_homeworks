using Domain.Entities;
using PropertiesApi.Dtos.RoomAmentities;

namespace PropertiesApi.Mappers;

internal static class RoomAmentityMapper
{
    internal static List<RoomAmentityDto> ToDto( this ICollection<RoomAmentity> roomAmentities )
    {
        return roomAmentities.Select( ra => ra.ToDto() ).ToList();
    }

    internal static RoomAmentityDto ToDto( this RoomAmentity roomAmentity )
    {
        return new RoomAmentityDto()
        {
            Id = roomAmentity.Id,
            Name = roomAmentity.Name
        };
    }
}
