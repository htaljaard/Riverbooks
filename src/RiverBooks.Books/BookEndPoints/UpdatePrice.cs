

using FastEndpoints;

namespace RiverBooks.Books;

internal class UpdatePrice(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/api/books/{Id}/pricehistory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
    {
        await _bookService.UpdatePriceAsync(req.Id, req.Price);

        var updatedbook = await _bookService.GetBookAsync(req.Id);

        await SendAsync(updatedbook, cancellation: ct);
    }

}

internal record UpdateBookPriceRequest(Guid Id, decimal Price);
