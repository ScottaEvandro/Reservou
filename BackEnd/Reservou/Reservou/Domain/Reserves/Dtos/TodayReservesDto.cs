namespace Reservou.Domain.Reserves.Dtos;

#nullable disable
public class TodayReservesDto
{
    public string SpaceName {get; set; }
    public string UserName {get; set; }
    public string UserPhone {get; set; }
    public string UserMail {get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
