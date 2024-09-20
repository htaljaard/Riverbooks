using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.EndPoint
{
    internal class Create : Endpoint<CreateUserRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Create(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override void Configure()
        {
            Post("/api/users");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {

            var user = new ApplicationUser
            {
                UserName = req.Email,
                Email = req.Email
            };

            await _userManager.CreateAsync(user, req.Password);
            await SendOkAsync();
        }
    }
}
