namespace Domain.Entities;

public sealed class Property
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public IList<RoomType> RoomTypes { get; set; } = [];
    public IList<Reservation> Reservations { get; set; } = [];
}
