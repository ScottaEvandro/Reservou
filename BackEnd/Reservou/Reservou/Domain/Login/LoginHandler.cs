using Reservou.Domain.Login.Infrastructure;

namespace Reservou.Domain.Login;

public class LoginHandler
{
    private readonly LoginQueries _loginQueries;

    public LoginHandler(LoginQueries loginQueries)
    {
        _loginQueries = loginQueries;
    }

    public async Task<bool> LoginStatus(string username, string password)
    {
        Console.WriteLine("Validando dados de login com o banco de dados");

        bool isValid = await _loginQueries.isValidLogin(username, password);

        return isValid;
    }
}
