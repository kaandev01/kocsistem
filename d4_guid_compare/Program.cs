using System.Diagnostics;

if (!Console.IsOutputRedirected)
    Console.Clear();
Console.WriteLine("======================================");
Console.WriteLine("       GUID FILE COMPARISON");
Console.WriteLine("======================================");

int initialGuidCount = ReadPositiveNumber(
    "How many GUIDs do you want to generate? ");

string sourceDirectory = ReadDirectory(
    "Source directory (press Enter for ./source): ",
    "source");

string destinationDirectory = ReadDirectory(
    "Destination directory (press Enter for ./destination): ",
    "destination");

string sourceFile = Path.Combine(sourceDirectory, "tasks.txt");
string copiedFile = Path.Combine(destinationDirectory, "tasks.txt");

Stopwatch stopwatch = Stopwatch.StartNew();

using (StreamWriter writer = new(sourceFile, append: false))
{
    for (int i = 0; i < initialGuidCount; i++)
        writer.WriteLine(Guid.NewGuid());
}

stopwatch.Stop();
ShowSuccess(
    $"{initialGuidCount:N0} GUIDs were written to {sourceFile}",
    stopwatch.Elapsed);

stopwatch.Restart();
File.Copy(sourceFile, copiedFile, overwrite: true);
stopwatch.Stop();
ShowSuccess($"The file was copied to {copiedFile}", stopwatch.Elapsed);

int additionalGuidCount = ReadPositiveNumber(
    "How many new GUIDs should be added to the copied file? ");

stopwatch.Restart();

using (StreamWriter writer = new(copiedFile, append: true))
{
    for (int i = 0; i < additionalGuidCount; i++)
        writer.WriteLine(Guid.NewGuid());
}

stopwatch.Stop();
ShowSuccess(
    $"{additionalGuidCount:N0} new GUIDs were added to the copied file.",
    stopwatch.Elapsed);

stopwatch.Restart();

HashSet<string> originalGuids = new(
    File.ReadLines(sourceFile),
    StringComparer.OrdinalIgnoreCase);

List<string> newGuids = File.ReadLines(copiedFile)
    .Where(guid => !originalGuids.Contains(guid))
    .ToList();

stopwatch.Stop();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine();
Console.WriteLine("--------------------------------------");
Console.WriteLine($"New GUID count: {newGuids.Count:N0}");
Console.WriteLine($"Comparison time: {stopwatch.Elapsed.TotalMilliseconds:N3} ms");
Console.WriteLine("--------------------------------------");
Console.ResetColor();

int previewCount = Math.Min(newGuids.Count, 10);

if (previewCount > 0)
{
    Console.WriteLine($"First {previewCount} new GUID(s):");

    foreach (string guid in newGuids.Take(previewCount))
        Console.WriteLine(guid);
}

static int ReadPositiveNumber(string message)
{
    while (true)
    {
        Console.Write(message);
        string? input = Console.ReadLine()?.Trim();

        if (int.TryParse(input, out int number) && number > 0)
            return number;

        ShowError("Please enter a positive whole number.");
    }
}

static string ReadDirectory(string message, string defaultDirectoryName)
{
    while (true)
    {
        Console.Write(message);
        string? input = Console.ReadLine()?.Trim();

        string directory = string.IsNullOrWhiteSpace(input)
            ? Path.Combine(Environment.CurrentDirectory, defaultDirectoryName)
            : Path.GetFullPath(input);

        try
        {
            Directory.CreateDirectory(directory);
            return directory;
        }
        catch (Exception exception) when (
            exception is IOException
            or UnauthorizedAccessException
            or ArgumentException
            or NotSupportedException)
        {
            ShowError($"The directory could not be created: {exception.Message}");
        }
    }
}

static void ShowSuccess(string message, TimeSpan elapsed)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{message} ({elapsed.TotalMilliseconds:N3} ms)");
    Console.ResetColor();
}

static void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {message}");
    Console.ResetColor();
}
