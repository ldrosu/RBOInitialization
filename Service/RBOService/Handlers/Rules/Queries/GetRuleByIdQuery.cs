using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Queries
{
    public class GetRuleByIdQuery: Query<Rule>
    {
        public Guid RuleId { get; set; }
    }
}
