using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class CustomStringComparer : ICustomStringComparer
{
    public int Compare(string x, string y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentException("Arguments cannot be null");            
        }

        ReadOnlySpan<char> xSpan = x.AsSpan();
        ReadOnlySpan<char> ySpan = y.AsSpan();

        int xDotIndex = xSpan.IndexOf(". ");
        int yDotIndex = ySpan.IndexOf(". ");

        if (xDotIndex == -1 || yDotIndex == -1)
        {
            throw new ArgumentException("String format is invalid");            
        }

        ReadOnlySpan<char> xText = xSpan.Slice(xDotIndex + 2);
        ReadOnlySpan<char> yText = ySpan.Slice(yDotIndex + 2);

        int textComparison = xText.CompareTo(yText, StringComparison.InvariantCulture);

        if (textComparison != 0)
        {
            return textComparison;            
        }

        ReadOnlySpan<char> xNumberSpan = xSpan.Slice(0, xDotIndex);
        ReadOnlySpan<char> yNumberSpan = ySpan.Slice(0, yDotIndex);

        if (long.TryParse(xNumberSpan, out long xNumber) && long.TryParse(yNumberSpan, out long yNumber))
        {
            return xNumber.CompareTo(yNumber);
        }
        else
        {
            throw new ArgumentException("Numeric part of the string is invalid");
        }
    }
}