using System.Globalization;

Console.Clear();
Console.WriteLine("================================");
Console.WriteLine("         C# CALCULATOR");
Console.WriteLine("================================");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1 - Addition");
    Console.WriteLine("2 - Subtraction");
    Console.WriteLine("3 - Multiplication");
    Console.WriteLine("4 - Division");
    Console.WriteLine("Q - Quit");
    Console.WriteLine("--------------------------------");
    Console.Write("Your choice: ");

    string? choice = Console.ReadLine()?.Trim().ToLowerInvariant();

    if (choice == "q")
        break;

    if (choice is not ("1" or "2" or "3" or "4"))
    {
        CalculatorHelper.ShowError("Please select 1, 2, 3, 4 or Q.");
        continue;
    }

    double? firstNumber = CalculatorHelper.ReadNumber("First number: ");

    if (firstNumber is null)
        continue;

    double? secondNumber = CalculatorHelper.ReadNumber("Second number: ");

    if (secondNumber is null)
        continue;

    if (choice == "4" && secondNumber == 0)
    {
        CalculatorHelper.ShowError("A number cannot be divided by zero.");
        continue;
    }

    string symbol = choice switch
    {
        "1" => "+",
        "2" => "-",
        "3" => "*",
        "4" => "/",
        _ => throw new InvalidOperationException()
    };

    double result = choice switch
    {
        "1" => firstNumber.Value + secondNumber.Value,
        "2" => firstNumber.Value - secondNumber.Value,
        "3" => firstNumber.Value * secondNumber.Value,
        "4" => firstNumber.Value / secondNumber.Value,
        _ => throw new InvalidOperationException()
    };

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("--------------------------------");
    Console.WriteLine(
        $"{firstNumber.Value} {symbol} {secondNumber.Value} = {result}");
    Console.ResetColor();
}

Console.WriteLine();
Console.WriteLine("Thank you for using the calculator!");

