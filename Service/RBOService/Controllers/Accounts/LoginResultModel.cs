using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Controllers.Account
{
    public class LoginResultModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
