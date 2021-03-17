using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Accounts.Commands
{
    public class LoginResult
    {
        public string Username { get; set; }        
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
