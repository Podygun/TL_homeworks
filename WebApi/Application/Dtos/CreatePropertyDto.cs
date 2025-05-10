namespace Application.Dtos;

public class CreatePropertyDto
{
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public CreatePropertyDto( string name, string country,
        string city, string address, double latitude, double longitude )
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}
