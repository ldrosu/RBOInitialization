using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RBOService.Exceptions;
using RBOService.Handlers.Accounts.Commands;
using RBOService.Infrastructure;
using SimpleSoft.Mediator;

namespace RBOService.Controllers.Account
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly AuthManager _jwtAuthManager;
        private readonly IMediator _mediator;

        public AccountsController(ILogger<AccountsController> logger, AuthManager jwtAuthManager, IMediator mediator)
        {
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginModel request, CancellationToken ct)
        {
            try
            {
                
                _logger.LogInformation($"User '{request.Username}' is trying to log in");

                var result = await _mediator.SendAsync(new LoginCommand
                {
                    Username = request.Username,
                    Password = request.Password
                }, ct);

                var resultModel = new LoginResultModel
                {
                    Username = request.Username,
                    Role = result.Role,
                    AccessToken = result.AccessToken
                };
                
                return Ok(resultModel);
            }
            catch (NotFoundException)
            {
                _logger.LogInformation($"Invalid Username or password");
                return Unauthorized();
            }   
        }
    }   
}
