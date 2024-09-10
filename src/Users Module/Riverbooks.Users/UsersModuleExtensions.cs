using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Data;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{

    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration,ILogger logger, List<System.Reflection.Assembly> mediatorAssemblies)
    {

        string? conncetionString = configuration.GetConnectionString("UsersConnectionString");

        services.AddDbContext<UsersDBContext>(options => options.UseSqlServer(conncetionString));
       
        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDBContext>();

        services.AddScoped<IApplicationUserRepository, EFApplicationUserRepository>();

        mediatorAssemblies.Add(typeof(UsersModuleExtensions).Assembly);

        logger.Information("{Module} Module added","Users");
        
        return services;
    }
}
