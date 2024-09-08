

using FastEndpoints;
using FluentValidation;

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

public record UpdateBookPriceRequest(Guid Id, decimal Price);


public class UpdateBookPriceRequestValidator : AbstractValidator<UpdateBookPriceRequest>
{
    public UpdateBookPriceRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}