﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message = null):base(message)
        {
        }
    }
}
