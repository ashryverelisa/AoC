namespace Aoc.Solutions._2025;

public sealed class Day03 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        return SumMaxBankJoltage(input, selectionLength: 2).ToString();
    }

    public string SolvePart2(string[] input)
    {
        return SumMaxBankJoltage(input, selectionLength: 12).ToString();
    }

    private static long SumMaxBankJoltage(IEnumerable<string> input, int selectionLength)
    {
        return input.Select(line => line.Trim()).Where(trimmed => trimmed.Length != 0).Sum(trimmed => FindMaxBankJoltage(trimmed, selectionLength));
    }

    private static long FindMaxBankJoltage(string digits, int selectionLength)
    {
        if (selectionLength <= 0)
        {
            throw new InvalidDataException("Selection length must be positive.");
        }

        if (digits.Length < selectionLength)
        {
            throw new InvalidDataException("Each bank must contain enough digits for the selection size.");
        }

        var parsedDigits = new int[digits.Length];
        for (var index = 0; index < digits.Length; index++)
        {
            parsedDigits[index] = ParseDigit(digits[index]);
        }

        var removalsRemaining = digits.Length - selectionLength;
        var stack = new int[digits.Length];
        var stackSize = 0;

        foreach (var digit in parsedDigits)
        {
            while (removalsRemaining > 0 && stackSize > 0 && stack[stackSize - 1] < digit)
            {
                stackSize--;
                removalsRemaining--;
            }

            stack[stackSize++] = digit;
        }

        stackSize -= removalsRemaining;

        long value = 0;
        for (var index = 0; index < selectionLength; index++)
        {
            value = value * 10 + stack[index];
        }

        return value;
    }

    private static int ParseDigit(char symbol)
    {
        if (!char.IsDigit(symbol))
        {
            throw new InvalidDataException($"Invalid digit '{symbol}'.");
        }

        return symbol - '0';
    }
}
