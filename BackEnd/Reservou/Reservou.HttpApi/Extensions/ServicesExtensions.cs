using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Cadastro;
using Reservou.Domain.Cadastro.Infrastructure;
using Reservou.Domain.Login;
using Reservou.Domain.Login.Infrastructure;

namespace Reservou.HttpApi.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddCorsDefinition(this IServiceCollection services)
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

        service.AddScoped<CadastroHandler>();
        service.AddScoped<CadastroRepository>();

        return service;
    }
}
