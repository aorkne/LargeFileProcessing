using FileSorter.Interfaces;

namespace FileSorter.Implementations.FileSorters;

public sealed class Merger : IMerger
{
    private readonly ICustomStringComparer _customStringComparer;

    public Merger(ICustomStringComparer customStringComparer)
    {
        _customStringComparer = customStringComparer;
    }

    public void MergeFiles(List<string> inputFiles, string outputFile)
    {
        // Priority queue to keep track of the smallest current lines across all files
        var priorityQueue = new SortedDictionary<string, List<Queue<(string line, StreamReader reader)>>>(_customStringComparer);

        // Initialize StreamReaders for each input file
        List<StreamReader> readers = inputFiles.Select(file => new StreamReader(file)).ToList();

        try
        {
            // Seed the priority queue with the first line from each reader
            foreach (var reader in readers)
            {
                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (!priorityQueue.ContainsKey(line))
                    {
                        priorityQueue[line] = new List<Queue<(string, StreamReader)>>();
                    }

                    var queue = priorityQueue[line].FirstOrDefault();
                    if (queue == null)
                    {
                        queue = new Queue<(string, StreamReader)>();
                        priorityQueue[line].Add(queue);
                    }

                    queue.Enqueue((line, reader));
                }
            }

            // Create StreamWriter to write the merged output
            using (var writer = new StreamWriter(outputFile))
            {
                // Continue merging until all queues are empty
                while (priorityQueue.Any())
                {
                    // Get the smallest current line
                    var first = priorityQueue.First();
                    var queue = first.Value.First();
                    var (line, currentReader) = queue.Dequeue();
                    if (queue.Count == 0)
                    {
                        first.Value.Remove(queue);
                        if (first.Value.Count == 0)
                        {
                            priorityQueue.Remove(first.Key);
                        }
                    }

                    // Write the smallest line to the output file
                    writer.WriteLine(line);

                    // Read the next line from the current reader and add it to the queue
                    if (!currentReader.EndOfStream)
                    {
                        var nextLine = currentReader.ReadLine();

                        if (!priorityQueue.ContainsKey(nextLine))
                        {
                            priorityQueue[nextLine] = new List<Queue<(string, StreamReader)>>();
                        }

                        var nextQueue = priorityQueue[nextLine].FirstOrDefault();
                        if (nextQueue == null)
                        {
                            nextQueue = new Queue<(string, StreamReader)>();
                            priorityQueue[nextLine].Add(nextQueue);
                        }

                        nextQueue.Enqueue((nextLine, currentReader));
                    }
                }
            }
        }
        finally
        {
            // Clean up
            foreach (var reader in readers)
            {
                reader.Dispose();
            }
        }
    }
}