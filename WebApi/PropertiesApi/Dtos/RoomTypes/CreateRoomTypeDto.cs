namespace PropertiesApi.Dtos.RoomTypes;

public sealed class CreateRoomTypeDto
{
    public int PropertyId { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = null!;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
}
