using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public long Index { get; set; }
        public string Value { get; set; }
        public Guid RuleId { get; set; }
    }
}
