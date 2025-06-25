namespace Domain.Entities;

public sealed class Reservation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }

    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }

    public decimal Total { get; set; }
    public string Currency { get; set; }

    public int NightsCount => ( DepartureDateTime - ArrivalDateTime ).Days;
}

