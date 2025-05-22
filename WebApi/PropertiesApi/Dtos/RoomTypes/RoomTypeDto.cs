using PropertiesApi.Dtos.RoomAmentities;
using PropertiesApi.Dtos.RoomServices;

namespace PropertiesApi.Dtos.RoomTypes;

public sealed class RoomTypeDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = null!;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }

    public IList<RoomServiceDto> RoomServicesDto { get; set; } = [];
    public IList<RoomAmentityDto> RoomAmentitiesDto { get; set; } = [];
}
