namespace FileSorter.Interfaces;

public interface IFileWriter
{
    void WriteLines(Memory<string> lines, string filePath);
}