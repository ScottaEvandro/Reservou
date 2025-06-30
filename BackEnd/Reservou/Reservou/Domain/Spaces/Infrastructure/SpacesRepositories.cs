using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Spaces.Commands;
using Reservou.Helpers;

namespace Reservou.Domain.Spaces.Infrastructure;

public class SpacesRepositories : BaseConnection
{
    public SpacesRepositories(IConfiguration configuration) : base(configuration) { }

    public async Task<bool> SaveNewSpace(NewSpaceCommand newSpace)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        try
        {
            string sql = $@"INSERT INTO espacos (
                                    nome,
                                    descricao,
                                    capacidade,
                                    tipo_id,
                                    valor_reserva,
                                    duracao_padrao,
                                    tempo_manutencao,
                                    ativo )
                                VALUES (
                                    @space_name,
                                    @description,
                                    @capacity,
                                    @space_type_id,
                                    @price,
                                    @reserve_duration,
                                    @maintenance_time,
                                    @is_active ) ";

            var result = await connection.ExecuteAsync(sql,
                new
                {
                    space_name = newSpace.SpaceName,
                    description = newSpace.Description,
                    capacity = newSpace.Capacity,
                    space_type_id = newSpace.SpaceTypeId,
                    price = newSpace.Price,
                    reserve_duration = newSpace.ReserveDuration,
                    maintenance_time = newSpace.MaintenanceTime,
                    is_active = newSpace.isActive
                });

            return result > 0;
        }
        catch (Exception ex)
        {
            return false;
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> SaveSpaceImages(List<string> spaceImages, int spaceId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sqlImages = $@"INSERT INTO espacos_fotos (
                                        espaco_id, 
                                        url )
                                    VALUES (
                                        @espaco_id,
                                        @url ) ";

        var result = await connection.ExecuteAsync(sqlImages, new { espaco_id = spaceId, url = spaceImages });

        return result > 0;
    }
}
