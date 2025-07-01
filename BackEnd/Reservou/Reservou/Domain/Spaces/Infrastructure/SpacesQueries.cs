using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Spaces.Dtos;
using Reservou.Helpers;
using System.Text.Json;

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

    public async Task<IEnumerable<GetAllSpacesDto>> GetAllSpaces()
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = $@"SELECT  e.id Id,
                                e.nome Name,
                                e.descricao Description,
                                e.capacidade Capacity,
                                e.tipo_id TypeId,
                                e.valor_reserva Price,
                                e.duracao_padrao ReserveTime,
                                e.tempo_manutencao MaintenanceTime,
                                COALESCE((SELECT ef_sub.url
                                             FROM espacos_fotos ef_sub
                                             WHERE ef_sub.espaco_id = e.id)) AS ImageUrlsJson,
                                e.ativo isActive
                         FROM espacos e
                         inner join espacos_fotos ef on ef.espaco_id = e.id ";

        var result = await connection.QueryAsync<GetAllSpacesDto>(sql);

        if (result is null)
        {
            return new List<GetAllSpacesDto>();
        }

        var processedSpacesImages = new List<GetAllSpacesDto>();
        foreach (var space in result)
        {
            space.ImageUrls = !string.IsNullOrEmpty(space.ImageUrlsJson)
                                 ? JsonSerializer.Deserialize<List<string>>(space.ImageUrlsJson)
                                 : [];

            processedSpacesImages.Add(space);

            space.ImageUrlsJson = null;
        }

        return processedSpacesImages;
    }
}
