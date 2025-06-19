using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Helpers;

namespace Reservou.Domain.Login.Infrastructure;

public class LoginQueries : BaseConnection
{
    public LoginQueries(IConfiguration configuration) : base(configuration)
    { }

    public async Task<bool> isValidLogin(string username, string password)
    {
        using NpgsqlConnection connection = new(ConnectionString);

        string sql = $@"SELECT COUNT(*) 
                          FROM usuarios 
                         WHERE nome = @username 
                           AND senha = @password ";

        var result = await connection.ExecuteScalarAsync<int>(
            sql,
            new { username, password }
        );

        return result > 0;
    }
}
