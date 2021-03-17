using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Commands
{
    public class DeleteRuleCommand: Command
    {
        public Guid RuleId { get; set; }
    }
}
