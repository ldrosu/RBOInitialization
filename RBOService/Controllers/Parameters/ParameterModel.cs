using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Controllers.Parameters
{
    public class ParameterModel
    {
        public Guid Id { get; set; }
        public long Index { get; set; }
        public string Value { get; set; }
        public Guid RuleId { get; set; }
    }
}
