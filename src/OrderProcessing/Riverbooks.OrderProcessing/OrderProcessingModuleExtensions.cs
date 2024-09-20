using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Riverbooks.OrderProcessing.Data;
using Serilog;

namespace RiverBooks.Users;

public static class OrderProcessingModuleExtensions
{

    public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services, IConfiguration configuration, ILogger logger, List<System.Reflection.Assembly> mediatorAssemblies)
    {

        string? conncetionString = configuration.GetConnectionString("OrderProcessingConnectionString");

        services.AddDbContext<OrderProcessingDBContext>(options =>
        {
            options.UseSqlServer(conncetionString);
        });

        services.AddScoped<IOrderRepository, EFOrderProcessingRepository>();

        mediatorAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);

        logger.Information("{Module} Module added", "OrderProcessing");

        return services;
    }
}
