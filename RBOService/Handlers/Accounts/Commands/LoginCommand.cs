using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Accounts.Commands
{
    public class LoginCommand : Command<LoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
