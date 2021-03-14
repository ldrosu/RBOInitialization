using RBOService.Enums;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Commands
{
    public class UpdateRuleCommand : Command
    {
        public Guid ExternalId { get; set; }
        public long Index { get; set; }
        public string Pattern { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
        public Guid GroupId;
    }
}
