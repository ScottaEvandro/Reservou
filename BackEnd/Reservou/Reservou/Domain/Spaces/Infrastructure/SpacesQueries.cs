using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Spaces.Dtos;
using Reservou.Helpers;

namespace Reservou.Domain.Spaces.Infrastructure;

public class SpacesQueries : BaseConnection
{
    public SpacesQueries(IConfiguration configuration) : base(configuration) {}

    public async Task<IEnumerable<GetSpacesDto>> GetSpaces()
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = @"SELECT id Id,
                              nome Name
                         FROM tipos_espacos 
                        ORDER BY nome ";

        var result = await connection.QueryAsync<GetSpacesDto>(sql);

        return result;
    }

    public async Task<int> GetLastSpaceIdInserted()
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = $@"SELECT MAX(id)
                         FROM espacos 
                        WHERE ativo ";

        return await connection.QueryFirstAsync<int>(sql);
    }
}
