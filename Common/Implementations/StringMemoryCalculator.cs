using System.Text;
using Common.Interfaces;

namespace Common.Implementations;

public sealed class StringMemoryCalculator : IStringMemoryCalculator
{
    private readonly int _newLineSize = Encoding.UTF8.GetByteCount(Environment.NewLine);
    
    public int CalculateStringMemoryUsage(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        return Encoding.UTF8.GetByteCount(input) + _newLineSize;
    }
}