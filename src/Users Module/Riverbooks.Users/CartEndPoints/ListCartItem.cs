using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.CartEndPoints
{
    internal class ListCartItem : EndpointWithoutRequest<CartResponse>
    {
        private readonly IMediator _mediator;
        public ListCartItem(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/api/Cart");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                await SendUnauthorizedAsync();
                return;
            }

            var query = new ListCartItemQuery(emailAddress);

            var result = await _mediator.Send(query, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
                return;
            }

            var response = new CartResponse(result.Value);

            await SendAsync(response);

        }
    }
}


