using RBOLib.Assignments;
using System;
using System.Reflection;


namespace RBOLib.Initializations
{
    internal class InitializationRule
    {
        public string Pattern { get; set; }
        public IAssignment AssignmentAction { get; set; }
        public object[] Parameters { get; set; }
        
        public InitializationRule(string pattern, SourceTypeEnum source, params object[] parameters)
        {
            Pattern = pattern;
            AssignmentAction = AssignmentFactory.Create(source);
            Parameters = parameters;
        }

        public bool InitializeCount(MemberPath path, ref int count)
        {           
            if (path.Matches(Pattern))
            {
                count = (int)AssignmentAction.Assign(path.Content, Parameters);
                return true;
            }     
            return false;
        }

        public bool InitializeElement(MemberPath path, Array array, int index)
        {
            if (path.Matches(Pattern))
            {
                array.SetValue(AssignmentAction.Assign(path.Content, Parameters), index);
                return true;
            }
            return false;
        }

        public bool InitializeProperty(MemberPath path, Object obj, PropertyInfo propertyInfo)
        {
            if (path.Matches(Pattern))
            {
                propertyInfo.SetValue(obj, AssignmentAction.Assign(path.Content, Parameters));
                return true;
            }     
            return false;
        }

    }
}
