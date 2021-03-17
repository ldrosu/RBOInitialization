using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RBOService.Utils.PathUtil;

namespace RBOService.Initializations
{
    public class RBTokenInitializer
    {
        public List<RuleWithParameters> Rules { get; set; } = new List<RuleWithParameters>();
        public RBTokenInitializer(List<InitRule> rules)
        {
            foreach (var rule in rules)
            {
                Rules.Add(new RuleWithParameters(rule));
            }
        }
        public void Initialize(JToken token)
        {

            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var child in token.Children<JProperty>())
                        Initialize(child);
                    break;
                case JTokenType.Array:
                    var arrayElement = token.Children().First();
                    int n=1;
                    InitializeLength(token.Path, ref n);
                    for (int i = 1; i < n; i++)
                    {
                        var newArrayElement = arrayElement.DeepClone();
                        arrayElement.AddAfterSelf(newArrayElement);
                    }
                    
                    foreach (var child in token.Children())
                        Initialize(child);
                    break;
                case JTokenType.Property:
                    Initialize(((JProperty)token).Value);
                    break;

                default:
                    InitializeValue(token.Path, (JValue)token);
                    //Console.WriteLine($"{token.Path}, {((JValue)token).Value}");
                    break;
            }
        }
        public void InitializeLength(string path, ref int length)
        {
            for (int i = Rules.Count - 1; i >= 0; i--)
            {
                var rule = Rules[i];
                bool matched = rule.IsMatch(path);
                if (matched)
                {
                    length = int.Parse((string)rule.Assign(path));
                }
            }            
        }
        public void InitializeValue(string path, JValue value)
        {
            string memberPath = ToMemberPath(path);
            for (int i = Rules.Count - 1; i >= 0; i--)
            {
                var rule = Rules[i];
                bool matched = rule.IsMatch(memberPath);
                if (matched)
                {
                    JValue replacementValue = new JValue(rule.Assign(path));
                    value.Replace(replacementValue);
                    break;
                }
            }
        }
    }
}
