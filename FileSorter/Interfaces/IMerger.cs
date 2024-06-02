namespace FileSorter.Interfaces;

public interface IMerger
{
    void MergeFiles(List<string> inputFiles, string outputFile);
}