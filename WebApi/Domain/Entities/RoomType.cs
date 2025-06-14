namespace Domain.Entities;

public sealed class RoomType
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int AmountRooms { get; set; }

    public Property Property { get; set; }

    public IList<RoomService> RoomServices { get; set; } = [];
    public IList<RoomAmentity> RoomAmentities { get; set; } = [];
    public IList<Reservation> Reservations { get; set; } = [];
}
