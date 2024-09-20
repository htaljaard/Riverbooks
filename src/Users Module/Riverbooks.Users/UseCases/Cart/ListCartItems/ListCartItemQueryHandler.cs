using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndPoints.AddItem;

namespace RiverBooks.Users.UseCases.Cart.ListCartItems;

public class ListCartItemQueryHandler : IRequestHandler<ListCartItemQuery, Result<List<CartItemDTO>>>
{
    private readonly IApplicationUserRepository _repository;

    public ListCartItemQueryHandler(IApplicationUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<CartItemDTO>>> Handle(ListCartItemQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var cartItems = user.CartItems.Select(i => new CartItemDTO(i.Id, i.BookId, i.Description, i.Quantity, i.UnitPrice)).ToList();

        return Result.Success(cartItems);
    }
}

