using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters.Commands
{
    public class UpdateParameterCommand : Command
    {
        public Guid ExternalId { get; set; }
        public long Index { get; set; }
        public string Value { get; set; }
        public Guid RuleId;
    }
}
