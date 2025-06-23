using Reservou.HttpApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder
    .Services.AddInjectionService(configuration)
    .AddSwagger()
    .AddVersioning()
    .AddCorsDefinition()
    .AddControllers()
    .Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API Incrível v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
