using System;
using System.Collections.Generic;
using System.Text;

namespace RBOClientLib.DTOs.Parameters
{
    class CreateParameterRequest
    {
        public int Index { get; set; }
        public string Value { get; set; }
        public Guid RuleId { get; set; }
    }
}
