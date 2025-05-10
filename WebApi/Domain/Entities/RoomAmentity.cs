namespace Domain.Entities;

public sealed class RoomAmentity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public IList<RoomType> RoomTypes { get; set; } = [];

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
