using Common;
using FileGenerator;
using FileGenerator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = DependencyInjection.ConfigureServices();
var fileGenerator = serviceProvider.GetService<IFileGenerator>();

Console.WriteLine("Enter the size of the file to generate (in GB):");
if (long.TryParse(Console.ReadLine(), out long sizeInGb))
{
    string sourceFilesDirectory = Path.Combine(DirectoryHelper.FilesDirectory, Constants.SourceFilesDirectory);

    Directory.CreateDirectory(sourceFilesDirectory);

    string fileName = $"largefile_{DateTimeOffset.UtcNow.Ticks}.txt";
    string filePath = Path.Combine(sourceFilesDirectory, fileName);

    fileGenerator.GenerateFile(filePath, sizeInGb * Constants.OneGb);
    Console.WriteLine($"File generated: {fileName}");
}
else
{
    Console.WriteLine("Invalid input.");
}