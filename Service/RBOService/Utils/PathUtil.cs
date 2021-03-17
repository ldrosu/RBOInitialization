using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBOService.Utils
{
    public static class PathUtil
    {
        public static string RemoveIndexes(string path)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in path)
            {
                if (!(Char.IsDigit(c) || c == '[' || c == ']'))
                    builder.Append(c);
            }
            return builder.ToString();
        }
        public static string ToMemberPath(string path)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in path)
            {
                if (c == '[' || c == '.')
                    builder.Append('/');
                else if (c == ']')
                {
                    continue;
                }
                else
                    builder.Append(c);
            }
            return builder.ToString();
        }
    }
}
