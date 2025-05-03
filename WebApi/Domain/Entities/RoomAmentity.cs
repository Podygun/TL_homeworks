namespace Domain.Entities;

public sealed class RoomAmentity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public RoomAmentity( Guid id, string name )
    {
        Id = id;
        Name = name;
    }

    public RoomAmentity( string name )
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
