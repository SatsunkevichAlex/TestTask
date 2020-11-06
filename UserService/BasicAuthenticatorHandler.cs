using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using UserService.Models;
using UserService.Services;

namespace UserService
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthService _authServcie;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthService authServcice)
            : base(options, logger, encoder, clock
        )
        {
            _authServcie = authServcice;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            AuthModel auth;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(
                    Request.Headers["Authorization"]);
                var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialsBytes).
                    Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];
                auth = await _authServcie.Authenticate(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (auth == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, auth.Username)                
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
