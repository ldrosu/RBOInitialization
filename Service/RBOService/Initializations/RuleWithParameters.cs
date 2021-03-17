using RBOService.Initializations.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBOService.Initializations
{
    public class RuleWithParameters
    {
        public string Pattern { get; set; }
        public List<string> Parameters { get; set; }
        public IAssignment AssignmentAction { get; set; }
        
        public RuleWithParameters(InitRule initRule)
        {
            Pattern = initRule.Pattern;
            AssignmentAction = AssignmentFactory.Create(initRule.SourceType);
            Parameters = initRule.Parameters;

        }
        public bool IsMatch(string path)
        {
            return Regex.IsMatch(path, Pattern);
        }
        public object Assign(string path)
        {
            return AssignmentAction.Assign(path, Parameters);
        }

        
    }
}
