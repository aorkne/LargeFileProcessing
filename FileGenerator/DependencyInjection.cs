using Common.Implementations;
using Common.Interfaces;
using FileGenerator.Implementations.FileGenerators;
using FileGenerator.Implementations.StringGenerators;
using FileGenerator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileGenerator;

public static class DependencyInjection
{
    public static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<ILineGenerator, RandomLineGenerator>()
            .AddSingleton<IFileGenerator, LargeFileGenerator>()
            .AddSingleton<IStringMemoryCalculator, StringMemoryCalculator>()
            .BuildServiceProvider();
    }
}