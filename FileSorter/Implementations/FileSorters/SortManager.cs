using Common;
using Common.Interfaces;
using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class SortManager : ISortManager
{
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly ICustomStringComparer _customStringComparer;
    private readonly IMerger _merger;
    private readonly IStringMemoryCalculator _stringMemoryCalculator;

    public SortManager(
        IFileReader fileReader,
        IFileWriter fileWriter,
        ICustomStringComparer customStringComparer,
        IMerger merger,
        IStringMemoryCalculator stringMemoryCalculator)
    {
        _fileReader = fileReader;
        _fileWriter = fileWriter;
        _customStringComparer = customStringComparer;
        _merger = merger;
        _stringMemoryCalculator = stringMemoryCalculator;
    }

    public void SortFile(string sourceFilePath, string destinationFilePath)
    {
        var tempDir = Path.Combine(DirectoryHelper.FilesDirectory, Constants.TempDirectory);
        Directory.CreateDirectory(tempDir);

        // Break the file into manageable chunks and sort them
        var chunkFiles = new List<string>();
        int chunkIndex = 0;
        foreach (Span<string> chunk in ReadChunks(sourceFilePath))
        {
            var chunkFile = Path.Combine(tempDir, $"chunk_{chunkIndex++}.txt");
            
            chunk.Sort(_customStringComparer);
            _fileWriter.WriteLines(chunk, chunkFile);
            chunk.Clear();
            
            chunkFiles.Add(chunkFile);
        }

        // Merge the sorted chunks
        _merger.MergeFiles(chunkFiles, destinationFilePath);

        // Clean up temporary directory
        Directory.Delete(tempDir, true);
    }

    private IEnumerable<string[]> ReadChunks(string filePath)
    {
        List<string> currentChunk = new List<string>();
        int totalSize = 0;
        foreach (var line in _fileReader.ReadLines(filePath))
        {
            currentChunk.Add(line);
            totalSize += _stringMemoryCalculator.CalculateStringMemoryUsage(line);
            
            if (totalSize >= Constants.ChunkSize)
            {
                yield return currentChunk.ToArray();

                totalSize = 0;
                currentChunk = new List<string>();
            }
        }

        if (currentChunk.Count > 0)
        {
            yield return currentChunk.ToArray();            
        }
    }
}