using FastEndpoints;

namespace RiverBooks.Books;

internal class CreateBookEndPoint(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest request, CancellationToken ct)
    {
        var newBook = new BookDto(request.Id ?? Guid.NewGuid(), request.Title, request.Author, request.Year, request.Price);

        await _bookService.CreateBookAsync(newBook);

        await SendCreatedAtAsync<GetBookByIdEndPoint>(new { newBook.Id }, newBook, cancellation: ct);
    }
}
