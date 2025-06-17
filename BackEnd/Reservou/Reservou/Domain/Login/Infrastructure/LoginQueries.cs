using Npgsql;

namespace Reservou.Domain.Login.Infrastructure;

public class LoginQueries
{
    public LoginQueries() { }

    public async Task<bool> isValidLogin(string username, string password)
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=reservou");
    }
}
