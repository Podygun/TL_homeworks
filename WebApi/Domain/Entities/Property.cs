namespace Domain.Entities;

public sealed class Property
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public IList<RoomType> RoomTypes { get; set; } = [];


    public Property( string name, string country,
        string city, string address, double latitude, double longitude )
    {
        CheckParamsThrowsExceptionIfIncorrect( name, country, city, address, latitude, longitude );

        Id = Guid.NewGuid();
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }


    public Property( Guid id, string name, string country,
        string city, string address, double latitude, double longitude )
    {
        CheckParamsThrowsExceptionIfIncorrect( name, country, city, address, latitude, longitude );

        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }


    private void CheckParamsThrowsExceptionIfIncorrect
        ( string name, string country, string city, string address, double latitude, double longitude )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ArgumentNullException( $"\"{nameof( name )}\" cannot be empty or white spaces." );
        }

        if ( string.IsNullOrWhiteSpace( country ) )
        {
            throw new ArgumentNullException( $"\"{nameof( country )}\" cannot be empty or white spaces." );
        }

        if ( string.IsNullOrWhiteSpace( city ) )
        {
            throw new ArgumentNullException( $"\"{nameof( city )}\" cannot be empty or white spaces." );
        }

        if ( string.IsNullOrWhiteSpace( address ) )
        {
            throw new ArgumentNullException( $"\"{nameof( address )}\" cannot be empty or white spaces." );
        }

        if ( latitude < 0 )
        {
            throw new ArgumentOutOfRangeException( $"\"{nameof( address )}\" cannot be smaller then zero." );
        }

        if ( longitude < 0 )
        {
            throw new ArgumentOutOfRangeException( $"\"{nameof( longitude )}\" cannot be smaller then zero." );
        }

    }
}
