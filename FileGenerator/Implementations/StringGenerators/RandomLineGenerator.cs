using Common;
using FileGenerator.Interfaces;

namespace FileGenerator.Implementations.StringGenerators;

public sealed class RandomLineGenerator : ILineGenerator
{
    public string GenerateLine()
    {
        long randomNumber = Random.Shared.NextInt64(1L, long.MaxValue);
        string randomText = GenerateRandomText(Random.Shared.Next(Constants.MinTextLength, Constants.MaxTextLength));
        return $"{randomNumber}. {randomText}";
    }
    
    private string GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray()).Trim();
    }
}