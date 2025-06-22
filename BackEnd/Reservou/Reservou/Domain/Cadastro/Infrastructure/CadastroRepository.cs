using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Helpers;
using System.Net;

namespace Reservou.Domain.Cadastro.Infrastructure;

public class CadastroRepository : BaseConnection
{
    public CadastroRepository(IConfiguration configuration) : base(configuration)
    { }

    public async Task<HttpStatusCode> AddCadastro(CadastroCommand cadastro)
    {
        try
        {
            using NpgsqlConnection connection = new(ConnectionString);

            string sql = $@"INSERT INTO usuarios (nome, cpf, email, telefone, senha, tipo_usuario)
                               VALUES (@nome, @cpf, @email, @telefone, @senha, @tipo_usuario) ";

            var result = await connection.QueryFirstOrDefaultAsync(sql,
                param: new
                {
                    nome = cadastro.Nome,
                    cpf = cadastro.Cpf,
                    email = cadastro.Email,
                    telefone = cadastro.Telefone,
                    senha = cadastro.Senha,
                    tipo_usuario = cadastro.TipoUsuario
                });

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
