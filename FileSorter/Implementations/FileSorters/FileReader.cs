using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class FileReader : IFileReader
{
    public IEnumerable<string> ReadLines(string filePath)
    {
        using var fileStream = new StreamReader(filePath);
        while (fileStream.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}