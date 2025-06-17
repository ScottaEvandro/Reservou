namespace Reservou.Domain.Login;

public class LoginHandler
{
    public LoginHandler() { }

    public async Task<bool> LoginStatus(string username, string password)
    {
        Console.WriteLine("Validando dados de login com o banco de dados");

        return true;
    }
}
