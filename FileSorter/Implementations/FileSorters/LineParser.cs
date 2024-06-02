using System.Globalization;
using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class LineParser : ILineParser
{
    public (string text, long number) ParseLines(string line)
    {
        var index = line.IndexOf(". ", StringComparison.Ordinal);
        var numberPart = long.Parse(line.AsSpan(0, index), CultureInfo.InvariantCulture);
        var stringPart = line.Substring(index + 2);

        return (stringPart, numberPart);
    }
}