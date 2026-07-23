using System.Globalization;

public static class CalculatorHelper
{
    public static double? ReadNumber(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine()?.Trim();

        bool isValid = double.TryParse(
            input,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out double number);

        if (isValid)
            return number;

        ShowError("Please enter a valid number. Example: 12.5");
        return null;
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ResetColor();
    }
}