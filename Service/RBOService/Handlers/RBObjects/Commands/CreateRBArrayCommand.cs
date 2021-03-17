using Newtonsoft.Json.Linq;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBOService.Handlers.RBObjects
{
    public class CreateRBArrayCommand : Command<CreateRBArrayResult>
    {
        public JArray ArrayTemplate { get; set; }
        public int Length { get; set; }
        public Guid GroupId { get; set; }
    }
}
