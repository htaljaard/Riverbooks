using Ardalis.Result;
using FastEndpoints;
using MediatR;
using OrderProcessing.Contracts;
using RiverBooks.Users.UseCases.Cart.CheckOutCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.CartEndPoints.Checkout
{


    internal class CheckOut : Endpoint<CheckOutRequest, CheckoutResponse>
    {
        private readonly IMediator _mediator;

        public CheckOut(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/api/cart/checkout");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CheckOutRequest req, CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                await SendUnauthorizedAsync();
                return;
            }

            var command = new CheckOutCartCommand(emailAddress, req.ShippingAddressId, req.BillingAddressId);

            var result = await _mediator.Send(command, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
                return;
            }

            await SendOkAsync(new CheckoutResponse(result.Value));
        }
    }
}
