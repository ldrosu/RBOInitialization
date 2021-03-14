using System;
using System.Collections.Generic;
using System.Text;

namespace RBOService.Initializations.Assignments
{
    public interface IAssignment
    {
        object Assign (string path, List<string> parameters);
    }
}
