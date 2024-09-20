using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Users.UseCases.Cart.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Result>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private IMediator _mediator;

        public AddItemToCartCommandHandler(IApplicationUserRepository applicationUserRepository, IMediator mediator)
        {
            _applicationUserRepository = applicationUserRepository;
            _mediator = mediator;
        }
        public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }


            var query = new BookDetailsQuery(request.BookId);
            var bookResult = await _mediator.Send(query);

            if (bookResult.Status == ResultStatus.NotFound)
            {
                return Result.NotFound();
            }

            var bookDetails = bookResult.Value;

            var description = $"{bookDetails.Title} by {bookDetails.Author}";

            var cartItem = new CartItem(request.BookId, description, request.Quantity, bookDetails.Price);

            user.AddToCart(cartItem);

            await _applicationUserRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
