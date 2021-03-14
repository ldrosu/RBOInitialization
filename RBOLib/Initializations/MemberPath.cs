using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RBOLib.Initializations
{
    public class MemberPath
    {
        private const char separator = '/';
        private readonly string content;

        public string Content
        {
            get => content;
        }

        public MemberPath(string path)
        {
            content = path;
        }

        public MemberPath()
        {
            content = "";
        }

        public MemberPath AddName(string name)
        {
            return new MemberPath(content + separator + name);
        }

        public MemberPath AddArrayElement(int i)
        {
            return new MemberPath(content + separator + i.ToString());
        }

        public bool Matches(string pattern)
        {
            //The static method caches the last 15 compiled patterns.
            return Regex.IsMatch(this.content, pattern);
        }

        public MemberPath RemoveLast(int n = 1)
        {
            string[] elements = (content.Split(separator)).SkipLast(n).ToArray();
            return new MemberPath(String.Join(separator, elements));
        }

        public int GetIndex()
        {
            if (String.IsNullOrEmpty(content))
                return -1;

            string[] elements = content.Split(separator);
            string element = elements.Last<string>();
            int index;
            bool res = int.TryParse(element, out index);
            if (!res) index = -1;
            return index;
        }

        public MemberPath RemoveIndexes()
        {
            string[] elements = (content.Split(separator)).ToArray();
            List<string> namedElements = new List<string>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Length > 0)
                {
                    if (!Char.IsDigit(elements[i][0]))
                    {
                        namedElements.Add(elements[i]);
                    }
                }
            }
            return new MemberPath (String.Join(separator,namedElements.ToArray())); 

        }
        public void Print()
        {
            Console.WriteLine(content);
        }
    }
}
