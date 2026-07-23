using System.Diagnostics;

Console.WriteLine("=== GUID FILE COMPARISON ===");

const string sourceDirectory = "source";
const string targetDirectory = "target";

Directory.CreateDirectory(sourceDirectory);
Directory.CreateDirectory(targetDirectory);

string sourceFile = Path.Combine(sourceDirectory, "tasks.txt");
string targetFile = Path.Combine(targetDirectory, "tasks.txt");

int initialCount = ReadPositiveInt("Initial GUID count: ");

Stopwatch stopwatch = Stopwatch.StartNew();
WriteGuids(sourceFile, initialCount);
stopwatch.Stop();
ShowResult($"{initialCount:N0} GUIDs written", stopwatch.Elapsed);

stopwatch.Restart();
File.Copy(sourceFile, targetFile, overwrite: true);
stopwatch.Stop();
ShowResult("File copied", stopwatch.Elapsed);

int additionalCount = ReadPositiveInt("Additional GUID count: ");

stopwatch.Restart();
WriteGuids(targetFile, additionalCount, append: true);
stopwatch.Stop();
ShowResult($"{additionalCount:N0} GUIDs added", stopwatch.Elapsed);

stopwatch.Restart();

HashSet<string> sourceGuids = new(
    File.ReadLines(sourceFile),
    StringComparer.Ordinal);

int newGuidCount = 0;
List<string> preview = new(capacity: 10);

foreach (string guid in File.ReadLines(targetFile))
{
    if (sourceGuids.Contains(guid))
        continue;

    newGuidCount++;

    if (preview.Count < preview.Capacity)
        preview.Add(guid);
}

stopwatch.Stop();

Console.WriteLine();
ShowResult($"{newGuidCount:N0} new GUIDs found", stopwatch.Elapsed);

if (preview.Count > 0)
{
    Console.WriteLine($"First {preview.Count} new GUIDs:");

    foreach (string guid in preview)
        Console.WriteLine(guid);
}

static void WriteGuids(string filePath, int count, bool append = false)
{
    using StreamWriter writer = new(filePath, append);

    for (int i = 0; i < count; i++)
        writer.WriteLine(Guid.NewGuid());
}

static int ReadPositiveInt(string message)
{
    while (true)
    {
        Console.Write(message);

        if (int.TryParse(Console.ReadLine(), out int number) && number > 0)
            return number;

        Console.WriteLine("Error: Enter a positive whole number.");
    }
}

static void ShowResult(string message, TimeSpan elapsed) =>
    Console.WriteLine($"{message} ({elapsed.TotalMilliseconds:N3} ms)");
