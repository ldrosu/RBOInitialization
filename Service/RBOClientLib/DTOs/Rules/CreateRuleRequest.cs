using System;
using System.Collections.Generic;
using System.Text;

namespace RBOClientLib.DTOs.Rules
{
    class CreateRuleRequest
    {
        public int Index { get; set; }
        public string Pattern { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
        public Guid GroupId { get; set; }
    }
}
