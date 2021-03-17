using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Database
{
    public class RuleEntity
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; }
        public long Index { get; set; }
        public string Pattern { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
        public List<ParameterEntity> Parameters { get; set; } = new List<ParameterEntity>();
        public GroupEntity Group { get; set; }
    }
}
