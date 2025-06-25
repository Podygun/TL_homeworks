using ReservationApi.Dtos.RoomAmentities;
using ReservationApi.Dtos.RoomServices;

namespace ReservationApi.Dtos.RoomTypes;

public class RoomTypeDto
{
    public int Id { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }

    public IList<RoomServiceDto> RoomServices { get; set; } = [];
    public IList<RoomAmentityDto> RoomAmentities { get; set; } = [];
}
