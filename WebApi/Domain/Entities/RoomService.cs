namespace Domain.Entities;

public sealed class RoomService
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IList<RoomType> RoomTypes { get; set; } = [];
}
