namespace Domain.Entities;

public sealed class RoomType
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = null!;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public IEnumerable<RoomService> RoomServices { get; set; }
    public IEnumerable<RoomAmentity> RoomAmentities { get; set; }
}
