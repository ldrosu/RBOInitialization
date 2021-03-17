namespace RBOLib.Assignments
{
    internal class ValueAssignment : IAssignment
    {
        public object Assign(string path, params object[] parameters)
        {
            return parameters[0];
        }
    }
}
