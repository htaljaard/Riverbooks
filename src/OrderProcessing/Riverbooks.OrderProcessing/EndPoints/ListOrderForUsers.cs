using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Riverbooks.OrderProcessing.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riverbooks.OrderProcessing.EndPoints
{
    internal class ListOrderForUsers : EndpointWithoutRequest<ListOrdersForUserResponse>
    {
        private readonly IMediator _mediator;

        public ListOrderForUsers(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/api/orders");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            var query = new ListOrdersForUserQuery(emailAddress!);

            var result = await _mediator.Send(query, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var response = new ListOrdersForUserResponse();

            response.Orders = result.Value;
            await SendAsync(response);
        }
    }

}
