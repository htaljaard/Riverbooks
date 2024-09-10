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
    internal class AddItem : Endpoint<AddCartItemRequest>
    {
        private readonly IMediator _mediator;

        public AddItem(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Post("/api/cart");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                await SendUnauthorizedAsync();
                return;
            }

            var command = new AddItemToCartCommand(req.BookId, req.Quantity, emailAddress);

            var result = await _mediator.Send(command, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
                return;
            }

            await SendOkAsync();


        }
    }
}
