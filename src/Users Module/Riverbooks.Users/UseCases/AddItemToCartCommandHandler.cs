using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Result>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public AddItemToCartCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            //TODO: Get from books module
            var cartItem = new CartItem(request.BookId, "description", request.Quantity, 9.00m);
            user.AddToCart(cartItem);

            await _applicationUserRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
