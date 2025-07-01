namespace Reservou.HttpApi.ViewModels.Reserves;

public class ReserveViewModel
{
    public int SpaceId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Hours { get; set; }
}
