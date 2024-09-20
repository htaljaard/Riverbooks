using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Contracts;
using Riverbooks.OrderProcessing.Data;


namespace Riverbooks.OrderProcessing.Integrations
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderItems = request.OrderItems
                .Select(a => new OrderItem(a.BookId, a.Quantity, a.UnitPrice, a.Description))
                .ToList();

            var shippingAddress = new Address("123 Main St", "Anytown", "USA", "12345", "12", "123");
            var billingAddress = new Address("123 Main St", "Anytown", "USA", "12345", "12", "123");

            var order = Order.Create(request.UserId, shippingAddress, billingAddress, orderItems);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation("New Order Created with Id: {OrderId}", order.Id);

            return new OrderDetailsResponse(order.Id);
        }
    }
}
