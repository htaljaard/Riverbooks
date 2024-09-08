using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services, ConfigurationManager config,ILogger logger)
    {

        string? conncetionString = config.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BooksDBContext>(options => options.UseSqlServer(conncetionString));
        services.AddScoped<IBookRepository, EFBookRepository>();


        services.AddScoped<IBookService, BookService>();

        logger.Information("{Module} Module added", "Books");
        return services;
    }
}
