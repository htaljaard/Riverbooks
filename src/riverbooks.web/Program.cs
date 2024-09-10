using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.Users;
using Serilog;
using System.Reflection;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting application");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
                .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Auth:JwtSecret"]!)
                .AddAuthorization()
                .SwaggerDocument();




//module services

List<Assembly> mediatorAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, logger, mediatorAssemblies);
builder.Services.AddUsersModule(builder.Configuration, logger, mediatorAssemblies);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatorAssemblies.ToArray()));

var app = builder.Build();

app
    .UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();



public partial class Program { };