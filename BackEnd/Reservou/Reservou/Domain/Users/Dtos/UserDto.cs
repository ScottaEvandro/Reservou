namespace Reservou.Domain.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string TaxId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int UserType { get; set; } // 0: Normal, 1: Admin
}
