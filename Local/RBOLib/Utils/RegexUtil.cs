using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RBOLib.Utils
{
    internal static class RegexUtil
    {
        public static string ToRegex(string jsonPath)
        {
            string[] elements = jsonPath.Split('.');
            string regex = "";
            for (int i = 0; i < elements.Length; i++)
            {
                string element = elements[i];
                if (element == "Length")
                {
                    regex += '$';
                    break;
                }
                else if (element.Contains("[]"))
                {
                    element = element.Replace("[]", "");
                    element += @"/\d+";
                }
                else if (element.Contains("[") && element.Contains("]"))
                {

                    element = element.Replace("[", "/");
                    element = element.Replace("]", "");

                }
                if (i > 0)
                    regex += '/';
                regex += element;


            }
            return regex;
        }
    }
}
