namespace Reservou.Domain.Reserves.Command;

public class SaveReserveCommand
{
    public int SpaceId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Hours { get; set; }
}
