using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Database
{
    public class ParameterEntity
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; }
        public long Index { get; set; }
        public string Value { get; set; }
        public RuleEntity Rule { get; set; }
    }
}
