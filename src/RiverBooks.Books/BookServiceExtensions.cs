using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookModuleServices(this IServiceCollection services, ConfigurationManager config,ILogger logger, List<System.Reflection.Assembly> mediatorAssemblies)
    {

        string? conncetionString = config.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BooksDBContext>(options => options.UseSqlServer(conncetionString));
        services.AddScoped<IBookRepository, EFBookRepository>();


        services.AddScoped<IBookService, BookService>();

        mediatorAssemblies.Add(typeof(BookServiceExtensions).Assembly);

        logger.Information("{Module} Module added", "Books");
        return services;
    }
}
