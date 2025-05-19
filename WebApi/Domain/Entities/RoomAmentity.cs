namespace Domain.Entities;

public sealed class RoomAmentity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public IList<RoomType> RoomTypes { get; set; } = [];
}
