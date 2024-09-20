using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndPoints.AddItem;

namespace RiverBooks.Users.UseCases.Cart.ListCartItems;


public record ListCartItemQuery(string EmailAddress) : IRequest<Result<List<CartItemDTO>>>;

