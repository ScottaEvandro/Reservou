using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Helpers;

namespace Reservou.Domain.Cadastro.Infrastructure;

public class CadastroRepository : BaseConnection
{
    public CadastroRepository(IConfiguration configuration) : base(configuration) 
    { }
    
    public async Task<bool> AddCadastro(CadastroCommand cadastro)
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

        return true;
    }

    public async Task<bool> UpdateCadastro()
    {
        using NpgsqlConnection connection = new(ConnectionString);

        string sql = $@"";

        var result = await connection.QueryFirstOrDefaultAsync(sql);
        return true;
    }

    public async Task<bool> DeleteCadastro()
    {
        using NpgsqlConnection connection = new(ConnectionString);

        string sql = $@"";

        var result = await connection.QueryFirstOrDefaultAsync(sql);
        return true;
    }
}
