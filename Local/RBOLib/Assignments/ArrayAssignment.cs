using RBOLib.Initializations;
using System.Collections.Generic;


namespace RBOLib.Assignments
{
    internal class ArrayAssignment : IAssignment
    {
        static object locker = new object();        
        //Every property path has a different array index sequence
        static Dictionary<string, int> dict = new Dictionary<string, int>();

        public object Assign(string path, params object[] parameters)
        {
            string namedPath = (new MemberPath(path)).RemoveIndexes().Content;
            lock (locker)
            {               
                int n = parameters.Length;
                int i;
                if (dict.ContainsKey(namedPath))
                {
                    i = dict[namedPath];
                }
                else
                {
                    i = 0;
                    dict[namedPath] = 0;
                }
                object obj = parameters[i];
                i++;
                if (i == n) i = 0;
                dict[namedPath] = i;
                return obj;
            }
        }
    }
}
