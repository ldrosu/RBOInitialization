namespace RBOLib.Assignments
{
    internal interface IAssignment
    {
        object Assign (string path, params object[] parameters);
    }
}
