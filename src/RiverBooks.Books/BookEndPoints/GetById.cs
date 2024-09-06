using FastEndpoints;

namespace RiverBooks.Books;

internal class GetById(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/api/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest request, CancellationToken ct)
    {
        var book = await _bookService.GetBookAsync(request.Id);

        if (book is null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(book, cancellation: ct);
    }
}

internal class GetBookByIdRequest
{
    public Guid Id { get; set; }
}
