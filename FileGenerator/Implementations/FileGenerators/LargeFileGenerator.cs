using Common;
using Common.Interfaces;
using FileGenerator.Interfaces;

namespace FileGenerator.Implementations.FileGenerators;

public sealed class LargeFileGenerator : IFileGenerator
{
    private readonly ILineGenerator _stringGenerator;
    private readonly IStringMemoryCalculator _stringMemoryCalculator;

    public LargeFileGenerator(ILineGenerator stringGenerator, IStringMemoryCalculator stringMemoryCalculator)
    {
        _stringGenerator = stringGenerator;
        _stringMemoryCalculator = stringMemoryCalculator;
    }

    public void GenerateFile(string filePath, long sizeInBytes)
    {
        int linesCount = Random.Shared.Next(Constants.MinRandomLinesCount, Constants.MaxRandomLinesCount);
        List<string> lines = new List<string>(linesCount);
        for (int i = 0; i < linesCount; i++)
        {
            lines.Add(_stringGenerator.GenerateLine());
        }
        
        using var writer = new StreamWriter(filePath);
        long totalSize = 0;
        while (totalSize < sizeInBytes)
        {
            string randomLine = lines[Random.Shared.Next(0, linesCount-1)];
            writer.WriteLine(randomLine);
            totalSize += _stringMemoryCalculator.CalculateStringMemoryUsage(randomLine);
        }
    }
}