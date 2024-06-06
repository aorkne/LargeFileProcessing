using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class FileWriter : IFileWriter
{
    public void WriteLines(ReadOnlySpan<string> lines, string filePath)
    {
        using var fileStream = new StreamWriter(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            fileStream.WriteLine(lines[i]);
        }
    }
}