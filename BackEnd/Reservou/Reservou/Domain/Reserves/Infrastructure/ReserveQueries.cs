using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Reservou.Domain.Reserves.Dtos;
using Reservou.Helpers;

namespace Reservou.Domain.Reserves.Infrastructure;

public class ReserveQueries : BaseConnection
{
    public ReserveQueries(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<TodayReservesDto>?> GetTodayReserves()
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = $@"SELECT e.nome SpaceName,
                        		u.nome UserName,
                        		u.telefone UserPhone,
                        		u.email UserMail,
                        		r.data_inicio StartTime,
                        		r.data_fim EndTime
                        FROM reservas r
                        INNER JOIN usuarios u ON r.usuario_id = u.id
                        INNER JOIN espacos e ON r.espaco_id = e.id
                        WHERE r.data_inicio::date = current_date";

        var result = await connection.QueryAsync<TodayReservesDto>(sql);

        return result;
    }

    public async Task<TimeSpan> GetReserveDuration(int id)
    {
        using var connection = new NpgsqlConnection(ConnectionString);

        string sql = $@"SELECT duracao_padrao
                          FROM espacos
                         WHERE id = @id ";

        return await connection.QueryFirstAsync<TimeSpan>(sql, new { id });
    }
}
