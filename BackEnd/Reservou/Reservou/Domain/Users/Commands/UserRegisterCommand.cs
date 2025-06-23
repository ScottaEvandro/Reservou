namespace Reservou.Domain.Users.Commands;

#nullable disable
public class UserRegisterCommand
{
    public string Username { get; set; }
    public string TaxId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
