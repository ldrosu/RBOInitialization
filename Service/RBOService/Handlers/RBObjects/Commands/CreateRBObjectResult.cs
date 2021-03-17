using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBOService.Handlers.RBObjects
{
    public class CreateRBObjectResult
    {
        public JObject InitializedObject { get; set; }
    }
}
