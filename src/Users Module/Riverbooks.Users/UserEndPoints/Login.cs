using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UserEndPoints
{
    public record LoginRequest(string Email, string Password);
    internal class Login : Endpoint<LoginRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Login(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override void Configure()
        {
            Post("/api/users/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                await SendUnauthorizedAsync();
                return;
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!loginResult)
            {
                await SendUnauthorizedAsync();
                return;
            }

            // Generate token
            var jwtSecret = Config["Auth:JwtSecret"]!;

            var token = JwtBearer.CreateToken(options =>
            {
                options.SigningKey = jwtSecret;
                options.User.Claims.Add(new System.Security.Claims.Claim("EmailAddress", user.Email!));
            });

            await SendOkAsync(token);

        }

    }
}
