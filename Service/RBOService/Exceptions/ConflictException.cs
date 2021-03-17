using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Exceptions
{
    public class ConflictException:Exception
    {
        public ConflictException(string message = null):base(message)
        {
        }
    }
}
