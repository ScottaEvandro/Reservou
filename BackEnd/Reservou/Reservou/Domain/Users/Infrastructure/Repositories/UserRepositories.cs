using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Users.Commands;
using Reservou.Helpers;
using System.Net;

namespace Reservou.Domain.Users.Infrastructure.Repositories;

public class UserRepositories : BaseConnection
{
    public UserRepositories(IConfiguration configuration) : base(configuration) { }

    public async Task<HttpStatusCode> CreateNewUser(UserRegisterCommand newUser)
    {
        try
        {
            using NpgsqlConnection connection = new(ConnectionString);

            string sql = $@"INSERT INTO usuarios (nome, cpf, email, telefone, senha, tipo_usuario) 
                               VALUES (@nome, @cpf, @email, @telefone, @senha, @tipo_usuario) ";

            var result = await connection.QuerySingleOrDefaultAsync(sql, 
                param: new
                {
                    nome = newUser.Username,
                    cpf = newUser.TaxId,
                    email = newUser.Email,
                    telefone = newUser.PhoneNumber,
                    senha = newUser.Password,
                    tipo_usuario = 0
                }
            );

            return HttpStatusCode.Accepted;
        }
        catch (PostgresException ex)
        {
            if (ex.SqlState == "23505") // Unique violation error code
            {
                return HttpStatusCode.Conflict;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        catch (Exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}
