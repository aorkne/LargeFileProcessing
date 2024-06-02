namespace FileSorter.Interfaces;

public interface IFileReader
{
    IEnumerable<string> ReadLines(string filePath);
}