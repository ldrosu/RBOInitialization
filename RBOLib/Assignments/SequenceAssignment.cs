using RBOLib.Initializations;
using System.Collections.Generic;


namespace RBOLib.Assignments
{
    internal class SequenceAssignment : IAssignment
    {
        static Dictionary<string, int> dict = new Dictionary<string, int>();
        public object Assign(string path, params object[] parameters)
        {
            string namedPath = (new MemberPath(path)).RemoveIndexes().Content;
            
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

            object obj = i;
            i++;
            dict[namedPath] = i;
            return obj;
        }
    }
}
