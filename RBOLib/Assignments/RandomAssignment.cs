using RBOLib.Utils;
using System;

namespace RBOLib.Assignments
{
    internal class RandomAssignment : IAssignment
    {
        ThreadSafeRandom random = new ThreadSafeRandom();

        public object Assign(string path, params object[] parameters)
        {
            int min = 0;
            int max = 1;

            int nArgs = parameters.Length;
            if (nArgs == 1)
                max = (int)parameters[0];
            else if (nArgs == 2)
            {
                min = (int)parameters[0];
                max = (int)parameters[1];
            }
            return random.Next(min, max);
        }
    }
}
