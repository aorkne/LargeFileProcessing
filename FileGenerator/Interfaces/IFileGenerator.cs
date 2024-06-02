namespace FileGenerator.Interfaces;

public interface IFileGenerator
{
    void GenerateFile(string filePath, long sizeInBytes);
}