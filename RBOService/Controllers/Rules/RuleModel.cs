using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Controllers.Rules
{
    public class RuleModel
    {
        public Guid Id { get; set; }
        public long Index { get; set; }
        public string Pattern { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
        public Guid GroupId { get; set; } 
    }
}
