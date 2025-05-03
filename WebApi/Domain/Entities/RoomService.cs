namespace Domain.Entities;

public sealed class RoomService
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public RoomService( Guid id, string name )
    {
        Id = id;

        ArgumentNullException.ThrowIfNullOrWhiteSpace( "Paramter name can't be empty" );

        Name = name;
    }

    public RoomService( string name )
    {
        Id = Guid.NewGuid();

        ArgumentNullException.ThrowIfNullOrWhiteSpace( name );

        Name = name;
    }
}
