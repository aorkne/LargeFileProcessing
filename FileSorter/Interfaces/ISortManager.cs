namespace FileSorter.Interfaces;

public interface ISortManager
{
    void SortFile(string sourceFilePath, string destinationFilePath);
}