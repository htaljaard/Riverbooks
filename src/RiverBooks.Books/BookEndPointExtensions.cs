using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

public static class BookEndPointsExtensions
{
    public static void MapBookEndPoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.GetBooks();
        });
    }
}

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}

internal interface IBookService
{
    IEnumerable<BookDto> GetBooks();
}

internal class BookService : IBookService
{
    public IEnumerable<BookDto> GetBooks()
    {
        return [
            new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien", 1954),
            new BookDto(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien", 1954),
            new BookDto(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien", 1955)
        ];
    }
}

public record BookDto(Guid Id, string Title, string Author, int Year);