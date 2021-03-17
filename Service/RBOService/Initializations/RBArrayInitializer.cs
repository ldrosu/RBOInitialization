using Newtonsoft.Json.Linq;
using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBOService.Initializations
{
    public static class RBArrayInitializer
    {
        public static JArray Initialize(JArray arrayTemplate, int length, List<InitRule> rules)
        {
            var arrayRule = new InitRule
            {
                Pattern = "^$",
                SourceType = SourceTypeEnum.Value,
                DestinationType = DestinationTypeEnum.Array,
                Parameters = new List<string>() { length.ToString() }

            };
            rules.Add(arrayRule);

            JToken token = arrayTemplate.DeepClone();
            var initializer = new RBTokenInitializer(rules);
            initializer.Initialize(token);
            return (JArray)token;
        }
    }
}
