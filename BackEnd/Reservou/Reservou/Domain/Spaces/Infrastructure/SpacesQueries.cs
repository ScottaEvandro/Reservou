using Dapper;
using Microsoft.Extensions.Configuration;
using Reservou.Domain.Spaces.Dtos;
using Reservou.Helpers;

namespace Reservou.Domain.Spaces.Infrastructure;

public class SpacesQueries : BaseConnection
{
    public SpacesQueries(IConfiguration configuration) : base(configuration) {}

    public async Task<IEnumerable<GetSpacesDto>> GetSpaces()
    {
        using var connection = new Npgsql.NpgsqlConnection(ConnectionString);

        string sql = @"SELECT id Id,
                              nome Name
                         FROM tipos_espacos 
                        ORDER BY nome ";

        var result = await connection.QueryAsync<GetSpacesDto>(sql);

        return result;
    }
}
