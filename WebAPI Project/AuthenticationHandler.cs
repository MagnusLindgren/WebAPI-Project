using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebAPI_Project.Models;

namespace WebAPI_Project
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<User> _userManager;
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
             UserManager<User> userManager
            )
            : base(options, logger, encoder, clock)
        {
             _userManager = userManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //implementerar basic Authentication för att identifiera vilken användare osm gör api-anropet.
            /// kollar header-fältet
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");



            User user;
            string password;
            try
            {
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Aruthorication"]);
                var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
                var crendentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = crendentials[0];
                password = crendentials[1];

                user = await _userManager.FindByNameAsync(username);

            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var identity = new ClaimsIdentity(Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
         
        }
    }
}


