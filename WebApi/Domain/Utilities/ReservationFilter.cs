namespace Domain.Utilities;

public class ReservationFilter
{
    public int? PropertyId { get; set; }
    public int? RoomTypeId { get; set; }
    public DateOnly? ArrivalDateFrom { get; set; }
    public DateOnly? ArrivalDateTo { get; set; }
    public DateOnly? DepartureDateFrom { get; set; }
    public DateOnly? DepartureDateTo { get; set; }
    public string? GuestName { get; set; }
    public string? GuestPhone { get; set; }
    public decimal? MinTotal { get; set; }
    public decimal? MaxTotal { get; set; }
}
