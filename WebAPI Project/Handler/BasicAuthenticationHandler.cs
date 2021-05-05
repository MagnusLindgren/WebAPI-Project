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
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<User> _userManager;
        public BasicAuthenticationHandler(
           UserManager<User> userManager,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            
            : base(options, logger, encoder, clock)
        {
             _userManager = userManager;
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = "Basic";

            return base.HandleChallengeAsync(properties);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
                if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            User user;
            string password;
            try
            {
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
                var crendentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = crendentials[0];
                password = crendentials[1];

                user = await _userManager.FindByNameAsync(username);

                if (user == null ||
                    password == null ||
                    _userManager.CheckPasswordAsync(user, password) == null)

                {
                    AuthenticateResult.Fail("Invalid username or password");
                }
            }
            catch (Exception ex) 
            {
                return AuthenticateResult.Fail(ex.Message);
            }

            var claims = new[]          
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };
       
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
         
        }  
    }
}


