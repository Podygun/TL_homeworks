namespace Application.Dtos;

/// <summary>
/// Для передачи в get запросы
/// </summary>
public class PropertyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public PropertyDto( Guid id, string name, string country,
        string city, string address, double latitude, double longitude )
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public PropertyDto()
    {
    }
}