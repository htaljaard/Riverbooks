using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

internal class List(IBookService bookService) : EndpointWithoutRequest<GetBooksResponse>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await _bookService.ListBooksAsync();

        await SendAsync(new GetBooksResponse { Books = books }, cancellation: ct);
    }
}


public class GetBooksResponse
{
    public List<BookDto> Books { get; set; } = [];
}
