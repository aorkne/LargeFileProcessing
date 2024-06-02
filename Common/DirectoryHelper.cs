namespace Common;

public static class DirectoryHelper
{
    public static string FilesDirectory
    {
        get
        {
            string solutionDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(solutionDirectory, Constants.FilesDirectory);
        }
    }
}