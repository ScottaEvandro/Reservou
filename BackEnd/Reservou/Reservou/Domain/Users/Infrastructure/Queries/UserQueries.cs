using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Users.Dtos;
using Reservou.Helpers;

namespace Reservou.Domain.Users.Infrastructure.Queries;

public class UserQueries : BaseConnection
{

    public UserQueries(IConfiguration configuration) : base(configuration) { }

    public async Task<UserDto> GetUserAsync(string username, string password)
    {
        using NpgsqlConnection connection = new(ConnectionString);

        string sql = $@"SELECT id Id, 
                               nome Username, 
                               cpf TaxId, 
                               email Email, 
                               telefone PhoneNumber, 
                               tipo_usuario UserType
                          FROM usuarios
                         WHERE nome = @Username
                           AND senha = @Password ";

        var result = await connection.QueryFirstOrDefaultAsync<UserDto>(
            sql, new { 
                Username = username, 
                Password = password 
            }
        );

        return result!;
    }
}
