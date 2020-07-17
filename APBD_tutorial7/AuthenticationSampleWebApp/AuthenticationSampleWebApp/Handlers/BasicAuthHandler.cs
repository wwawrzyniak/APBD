using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using AuthenticationSampleWebApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuthenticationSampleWebApp.Services;
using AuthenticationSampleWebApp.DTOs.Requests;

namespace AuthenticationSampleWebApp.Handlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IStudentDbService _service;
        public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IStudentDbService service
            ) : base(options, logger, encoder, clock)
        {
            _service = service;
        }



        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing authorization header");

            //"Authorization: Basic slmdadsds"  =>  bajty -> "jan123:sd2swd"
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(":");

            if (credentials.Length != 2)
                return AuthenticateResult.Fail("Incorrect authorization header value");

            var logReq = new LoginRequestDto
            {
                Login = credentials[0],
                Haslo = credentials[1]
            };
            Console.Write(logReq.Login);

            //TODO check credentials in DB

            //  var result = _service.LoginCheck(logReq);
            // var okResult = result as OkObjectResult;

            // assert
            // if (result.Equals(okResult))
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, credentials[0]),
               // new Claim(ClaimTypes.Name, credentials[0]),
                new Claim(ClaimTypes.Role, "student")
            };

                var identity = new ClaimsIdentity(claims, Scheme.Name); //Basic
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);

                //  }
                //else return AuthenticateResult.Fail("Failed");
            }
        }
    }
}

