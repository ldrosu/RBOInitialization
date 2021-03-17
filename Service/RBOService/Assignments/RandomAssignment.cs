using RBOService.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace RBOService.Initializations.Assignments
{ 
    public class RandomAssignment : IAssignment
    {
        ThreadSafeRandom random = new ThreadSafeRandom();
        public object Assign(string path, List<string> parameters)
        {
            int min = 0;
            int max = 1;

            int nArgs = parameters.Count;
            if (nArgs == 1)
                max = int.Parse(parameters[0]);
            else if (nArgs == 2)
            {
                min = int.Parse(parameters[0]);
                max = int.Parse(parameters[1]);
            }
            return random.Next(min, max);
        }
    }
}
