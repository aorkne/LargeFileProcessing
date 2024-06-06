namespace FileSorter.Interfaces;

public interface IFileWriter
{
    void WriteLines(ReadOnlySpan<string> lines, string filePath);
}