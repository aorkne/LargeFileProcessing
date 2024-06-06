using System.Diagnostics;
using Common;
using FileSorter;
using FileSorter.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = DependencyInjection.ConfigureServices();
var sortManager = serviceProvider.GetService<ISortManager>();

Console.WriteLine("Enter the name of the file to sort:");
string inputFileName = Console.ReadLine();

if (string.IsNullOrEmpty(inputFileName))
{
    Console.WriteLine("Invalid input.");
    return;
}

string inputFilePath = Path.Combine(DirectoryHelper.FilesDirectory, Constants.SourceFilesDirectory, inputFileName);

if (!File.Exists(inputFilePath))
{
    Console.WriteLine("File not found.");
    return;
}

string outputFileName = $"sorted_{Path.GetFileName(inputFileName)}";
string outputFilePath = Path.Combine(DirectoryHelper.FilesDirectory, Constants.OutputFilesDirectory, outputFileName);
Directory.CreateDirectory(Path.Combine(DirectoryHelper.FilesDirectory, Constants.OutputFilesDirectory));

if (File.Exists(outputFileName))
{
    File.Delete(outputFilePath);
}

var tempDir = Path.Combine(DirectoryHelper.FilesDirectory, Constants.TempDirectory);
if (Directory.Exists(tempDir))
{
    Directory.Delete(tempDir, true);
}

var watch = Stopwatch.StartNew();

sortManager.SortFile(inputFilePath, outputFilePath);

Console.WriteLine($"Elapsed time: {watch.Elapsed}");

Console.WriteLine($"File sorted: {outputFilePath}");