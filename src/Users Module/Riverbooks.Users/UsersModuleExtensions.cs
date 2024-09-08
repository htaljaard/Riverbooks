using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{

    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration,ILogger logger)
    {

        string? conncetionString = configuration.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDBContext>(options => options.UseSqlServer(conncetionString));
        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDBContext>();
        logger.Information("{Module} Module added","Users");
        return services;
    }
}
