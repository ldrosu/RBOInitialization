using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters.Commands
{
    public class CreateParameterCommand: Command<CreateParameterResult>
    {
        public long Index { get; set; }
        public string Value { get; set; }
        public Guid RuleId { get; set; }
    }
}
