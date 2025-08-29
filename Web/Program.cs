using Business.Implements;
using Business.Interfaces;
using Data.Implements;
using Data.Implements.BaseData;
using Data.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Utilities.Mappers.Profiles;
using Web.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controllers y servicios
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

// Swagger
builder.Services.AddSwaggerDocumentation();

// DbContext
// DbContext dinámico según el proveedor
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var provider = builder.Configuration["DatabaseProvider"];
    switch (provider)
    {
        case "SqlServer":
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            );
            break;

        case "Postgres":
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("PostgresConnection")
            );
            break;

        case "MySql":
            options.UseMySql(
                builder.Configuration.GetConnectionString("MySqlConnection"),
                new MySqlServerVersion(new Version(8, 0, 39)) // versión de tu contenedor
            );
            break;

        default:
            throw new Exception("Proveedor de base de datos no soportado. Usa: SqlServer, Postgres o MySql en appsettings.json");
    }
});
    


// Repositorios y capa de negocio
builder.Services.AddScoped(typeof(IBaseModelData<>), typeof(BaseModelData<>));
builder.Services.AddScoped(typeof(IBaseBusiness<,>), typeof(BaseBusiness<,>));

builder.Services.AddScoped<IClienteData, ClienteData>();
builder.Services.AddScoped<IClienteBusiness, ClienteBusiness>();

builder.Services.AddScoped<IPizzaData, PizzaData>();
builder.Services.AddScoped<IPizzaBusiness, PizzaBusiness>();

builder.Services.AddScoped<IPedidoData, PedidoData>();
builder.Services.AddScoped<IPedidoBusiness, PedidoBusiness>();

// AutoMapper (registro automático de perfiles)
builder.Services.AddAutoMapper(typeof(ClienteProfile).Assembly);

var app = builder.Build();

// Manejo global de errores (opcional)
app.UseExceptionHandler("/error");

// Swagger (según entorno o config)
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Sistema de Gestión v1");
        c.RoutePrefix = string.Empty;
    });
}

// Middlewares
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Migraciones automáticas
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        dbContext.Database.Migrate();
        logger.LogInformation("Base de datos verificada y migraciones aplicadas exitosamente.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error durante la migración de la base de datos.");
    }
}

// Log de arranque
var loggerApp = app.Services.GetRequiredService<ILogger<Program>>();
loggerApp.LogInformation("Aplicación iniciada correctamente en entorno {Env}", app.Environment.EnvironmentName);

app.Run();
