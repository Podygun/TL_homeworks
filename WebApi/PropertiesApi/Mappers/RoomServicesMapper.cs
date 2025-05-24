using Domain.Entities;
using PropertiesApi.Dtos.RoomServices;

namespace PropertiesApi.Mappers;

internal static class RoomServiceMapper
{
    internal static List<RoomServiceDto> ToDto( this ICollection<RoomService> roomServices )
    {
        return roomServices.Select( rs => rs.ToDto() ).ToList();
    }

    internal static RoomServiceDto ToDto( this RoomService roomService )
    {
        return new RoomServiceDto()
        {
            Id = roomService.Id,
            Name = roomService.Name
        };
    }
}
