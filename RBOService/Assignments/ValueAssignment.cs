using System;
using System.Collections.Generic;
using System.Text;

namespace RBOService.Initializations.Assignments
{
    class ValueAssignment : IAssignment
    {
        public object Assign(string path, List<string> parameters)
        {
            return parameters[0];
        }
    }
}
