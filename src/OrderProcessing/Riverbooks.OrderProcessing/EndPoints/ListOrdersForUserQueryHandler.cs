using Ardalis.Result;
using MediatR;
using Riverbooks.OrderProcessing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverbooks.OrderProcessing.EndPoints
{
    internal record ListOrdersForUserQuery(string EmailAddress) : IRequest<Result<List<OrderSummary>>>;
    internal class ListOrdersForUserResponse
    {
        public List<OrderSummary> Orders { get; set; } = new();
    }
    internal class ListOrdersForUserQueryHandler : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.ListAsync();

            var summaries = orders.Select(o => new OrderSummary(UserId: o.UserId,
                                                                DateCreated: o.DateCreated,
                                                                OrderId: o.Id,
                                                                Total: o.OrderItems.Sum(oi => oi.UnitPrice))).ToList();

            return summaries;

        }
    }
}
