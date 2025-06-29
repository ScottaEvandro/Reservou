namespace Reservou.Domain.Spaces.Commands;

public class NewSpaceCommand
{
    public NewSpaceCommand(string SpaceName, string Description, int Capacity, int SpaceTypeId, decimal Price,
                           TimeSpan ReserveDuration, TimeSpan MaintenanceTime, string[] SpaceImages, bool isActive)
    {
        this.SpaceName = SpaceName;
        this.Description = Description;
        this.Capacity = Capacity;
        this.SpaceTypeId = SpaceTypeId;
        this.Price = Price;
        this.ReserveDuration = ReserveDuration;
        this.MaintenanceTime = MaintenanceTime;
        this.SpaceImages = SpaceImages ?? Array.Empty<string>();
        this.isActive = isActive;
    }

    public string SpaceName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int SpaceTypeId { get; set; }
    public decimal Price { get; set; }
    public TimeSpan ReserveDuration { get; set; }
    public TimeSpan MaintenanceTime { get; set; }
    public string[] SpaceImages { get; set; } = Array.Empty<string>();
    public bool isActive { get; set; }

}