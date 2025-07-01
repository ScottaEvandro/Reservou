using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Reserves.Command;
using Reservou.Helpers;

namespace Reservou.Domain.Reserves.Infrastructure;

public class ReserveRepositories : BaseConnection
{
    public ReserveRepositories(IConfiguration configuration) : base(configuration) { }

    public async Task<bool> SaveReserve(SaveReserveCommand reserve, TimeSpan reserveDuration)
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = $@"INSERT INTO reservas (
                                        usuario_id,
                                        espaco_id,
                                        data_inicio,
                                        data_fim,
                                        status
                                   ) VALUES (
                                        @userId,
                                        @spaceId,
                                        @startDate,
                                        @endData,
                                        @status )";

        var result = await connection.ExecuteAsync(sql, 
            new
            {
                userId = reserve.UserId,
                spaceId = reserve.SpaceId,
                startDate = reserve.StartDate.Add(reserve.Hours),
                endData = reserve.StartDate.Add(reserve.Hours.Add(reserveDuration)),
                status = 1
            });

        return result > 0;
    }
}
