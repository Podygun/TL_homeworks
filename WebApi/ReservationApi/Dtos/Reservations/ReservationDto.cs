using ReservationApi.Dtos.Properties;
using ReservationApi.Dtos.RoomTypes;

namespace ReservationApi.Dtos.Reservations;

public class ReservationDto
{
    public int Id { get; set; }

    public PropertyDto Property { get; set; }

    public RoomTypeDto RoomType { get; set; }

    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }

    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }

    public decimal Total { get; set; }
    public string Currency { get; set; }

    public int NightsCount => ( DepartureDateTime - ArrivalDateTime ).Days;
}
