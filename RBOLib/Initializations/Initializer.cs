using System;
using System.Collections.Generic;
using System.Reflection;

namespace RBOLib.Initializations
{
    public class Initializer
    {
        private List<InitializationRule> rules = new List<InitializationRule>();

        public void AddRule(string pattern, SourceTypeEnum source, params object[] parameters)
        {
            rules.Add(new InitializationRule(pattern, source, parameters));
        }

        public void InitializeProperty(MemberPath path, Object obj, PropertyInfo propertyInfo)
        {
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                bool matched = rules[i].InitializeProperty(path, obj, propertyInfo);
                if (matched) break;
            }

        }

        public void InitializeElement(MemberPath path,  Array array, int index)
        {
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                bool matched = rules[i].InitializeElement(path, array, index);
                if (matched) break;
            }
        }

        public int InitializeCount(MemberPath path)
        {
            int count = 0;
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                bool matched = rules[i].InitializeCount(path, ref count);
                if (matched) break;
            }
            return count;
        }
    }
}
