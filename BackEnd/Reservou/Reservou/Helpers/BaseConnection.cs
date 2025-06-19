using Microsoft.Extensions.Configuration;

namespace Reservou.Helpers;

public abstract class BaseConnection
{
    protected string ConnectionString { get; private set; }

    public BaseConnection(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("PostgreSql");
    }
}
