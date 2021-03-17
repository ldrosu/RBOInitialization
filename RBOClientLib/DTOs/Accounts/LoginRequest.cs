using System;
using System.Collections.Generic;
using System.Text;

namespace RBOClientLib.DTOs.Accounts
{
    class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
