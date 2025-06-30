namespace Reservou.Domain.Spaces.Dtos;

public class GetAllSpacesDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int TypeId { get; set; }
    public decimal Price { get; set; }
    public TimeSpan ReserveTime { get; set; }
    public TimeSpan MaintenanceTime { get; set; }
    public string ImageUrlsJson { get; set; }
    public List<string> ImageUrls { get; set; }
    public bool isActive { get; set; }
}
