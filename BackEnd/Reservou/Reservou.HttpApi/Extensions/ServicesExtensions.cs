using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Reservou.Domain.Users.Handlers;
using Reservou.Domain.Users.Infrastructure.Queries;
using Reservou.Domain.Users.Infrastructure.Repositories;

namespace Reservou.HttpApi.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddInjectionService(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddScoped<UserHandler>();
        service.AddScoped<UserQueries>();
        service.AddScoped<UserRepositories>();

        return service;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services) // Fix method signature
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Reservou API",
                Version = "v1",
                Description = "Reservou API Documentation",
                Contact = new OpenApiContact
                {
                    Name = "Seu Nome",
                    Email = "seu.email@example.com",
                    Url = new Uri("https://seusite.com")
                },
                License = new OpenApiLicense
                {
                    Name = "Licença de Exemplo",
                    Url = new Uri("https://example.com/license")
                }
            });
        });

        return services;
    }

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
                    builder.WithOrigins("http://127.0.0.1:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        return services;
    }
}
