using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RBOService.Exceptions;
using RBOService.Enums;
using System.Security.Claims;
using RBOService.Infrastructure;

namespace RBOService.Handlers.Accounts.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        private readonly ApiDbContext _context;
        private readonly AuthManager _jwtAuthManager;

        public LoginCommandHandler(ApiDbContext context, AuthManager jwtAuthManager)
        {
            _context = context;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<LoginResult> HandleAsync(LoginCommand cmd, CancellationToken ct)
        {
            var users = _context.Set<UserEntity>();
            var test = await users.Select(u => u).ToListAsync();
            var user = await users.Where(u => u.Username == cmd.Username && u.Password==cmd.Password).Select(u => u).FirstOrDefaultAsync();
           
            if (user!=null)
            {
                var role = "BasicUser";
                var username = user.Username;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                };

                var accessToken = _jwtAuthManager.GenerateTokens(claims);
                
                return new LoginResult
                {
                    Username = username,
                    Role = role,
                    AccessToken = accessToken
                };
            }
            else
            {
                throw new NotFoundException($"Incorect username '{cmd.Username}' or password");
                
            }
        }
    }
}
