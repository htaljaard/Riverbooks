using Ardalis.Result;
using MediatR;
using OrderProcessing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.Cart.CheckOutCart
{
    public record CheckOutCartCommand(string EmailAddress, Guid ShippingAddressId, Guid BillingAddressId) : IRequest<Result<Guid>>;

    public class CheckOutCartCommandHandler : IRequestHandler<CheckOutCartCommand, Result<Guid>>
    {
        private readonly IApplicationUserRepository _repository;
        private readonly IMediator _mediator;

        public CheckOutCartCommandHandler(IApplicationUserRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CheckOutCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            var summaryItems = user.CartItems.Select(i => new OrderItemDetails(i.BookId,
                                                                               i.Quantity,
                                                                               i.UnitPrice,
                                                                               i.Description)).ToList();

            var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
                                                            request.ShippingAddressId,
                                                            request.BillingAddressId,
                                                            summaryItems);

            var createOrderResult = await _mediator.Send(createOrderCommand);


            if (!createOrderResult.IsSuccess)
            {
                return createOrderResult.Map(x => x.OrderId);
            }

            user.ClearCart();
            await _repository.SaveChangesAsync();

            return Result.Success(createOrderResult.Value.OrderId);
        }
    }
}
