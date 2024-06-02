namespace FileSorter.Interfaces;

public interface ILineParser
{
    (string text, long number) ParseLines(string line);
}