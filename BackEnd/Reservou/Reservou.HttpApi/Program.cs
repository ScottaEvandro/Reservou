using Reservou.HttpApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder
    .Services.AddInjectionService(configuration)
    .AddVersioning()
    .AddCorsDefinition()
    .AddControllers()
    .Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
