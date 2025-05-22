using Domain.Entities;
using PropertiesApi.Dtos.Properties;

namespace PropertiesApi.Dtos.RoomTypes;

public sealed class RoomTypeDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = null!;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }

    public IList<RoomService> RoomServices { get; set; } = [];
    public IList<RoomAmentity> RoomAmentities { get; set; } = [];
}
