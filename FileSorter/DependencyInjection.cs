using Common.Implementations;
using Common.Interfaces;
using FileSorter.Implementations.FileSorters;
using FileSorter.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileSorter;

public static class DependencyInjection
{
    public static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<IFileReader, FileReader>()
            .AddSingleton<IFileWriter, FileWriter>()
            .AddSingleton<IMerger, Merger>()
            .AddSingleton<ISortManager, SortManager>()
            .AddSingleton<IStringMemoryCalculator, StringMemoryCalculator>()
            .AddSingleton<ICustomStringComparer, CustomStringComparer>()
            .BuildServiceProvider();
    }
}