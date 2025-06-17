using Reservou.Domain.Login;
using Reservou.Domain.Login.Infrastructure;
using Reservou.HttpApi.Controllers;
using System.Net.NetworkInformation;

namespace Reservou.HttpApi.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5501")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IServiceCollection AddInjectionService(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddScoped<LoginHandler>();
        service.AddScoped<LoginQueries>();

        return service;
    }
}
