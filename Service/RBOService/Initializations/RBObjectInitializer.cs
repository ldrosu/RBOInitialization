using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBOService.Initializations
{
    public static class RBObjectInitializer
    {
        public static JObject Initialize(JObject objectTemplate, List<InitRule> rules)
        {
            JToken token = objectTemplate.DeepClone();
            var initializer = new RBTokenInitializer(rules);            
            initializer.Initialize(token);
            return (JObject)token;
        }
    }
}
