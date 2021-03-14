﻿using System;
using System.Collections.Generic;
using System.Text;
using static RBOService.Utils.PathUtil;

namespace RBOService.Initializations.Assignments
{
    public class SequenceAssignment : IAssignment
    {
        static object locker = new object();
        static Dictionary<string, int> dict = new Dictionary<string, int>();
        public object Assign(string path, List<string> parameters)
        {
            string namedPath = RemoveIndexes(path);
            lock (locker)
            {
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
}
