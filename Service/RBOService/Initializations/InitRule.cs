using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Initializations
{
    public class InitRule
    {      
        public string Pattern { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DestinationTypeEnum DestinationType { get; set; }
        public List<string> Parameters { get; set; }
    }
}
