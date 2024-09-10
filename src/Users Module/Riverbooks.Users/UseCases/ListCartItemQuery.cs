using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndPoints;

namespace RiverBooks.Users.UseCases;


public record ListCartItemQuery(string EmailAddress) : IRequest<Result<List<CartItemDTO>>>;

