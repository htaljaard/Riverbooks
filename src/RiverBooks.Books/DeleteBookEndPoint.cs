using FastEndpoints;

namespace RiverBooks.Books;

internal class DeleteBookEndPoint(IBookService bookService) : Endpoint<DeleteBookRequest>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Delete("/api/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        await _bookService.DeleteBookAsync(req.Id);
        await SendNoContentAsync();
    }

}

public record DeleteBookRequest(Guid Id);