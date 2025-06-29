namespace Reservou.HttpApi.ViewModels.Spaces;

public class NewSpaceViewModel
{
    public string SpaceName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int SpaceTypeId { get; set; }
    public decimal Price { get; set; }
    public TimeSpan ReserveDuration { get; set; }
    public TimeSpan MaintenanceTime { get; set; }
    public string[] SpaceImages { get; set; } = [];
    public bool isActive { get; set; }
}
