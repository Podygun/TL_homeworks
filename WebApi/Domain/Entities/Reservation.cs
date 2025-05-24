namespace Domain.Entities;

public sealed class Reservation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }

    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }

    public decimal Total { get; set; }
    public string Currency { get; set; }

    public int NightsCount => ( DepartureDate - ArrivalDate ).Days;
}
