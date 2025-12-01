namespace Aoc.Solutions._2024;

public sealed class Day02 : IAocSolution
{
    private IReadOnlyList<int[]> _rows = null!;
    
    public string SolvePart1(string[] input)
    {
        MappingRows(input);
        return RunCheck().ToString();
    }

    public string SolvePart2(string[] input)
    {
        MappingRows(input);
        return RunCheck(true).ToString();
    }

    private void MappingRows(string[] input)
    {
        _rows = input
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .ToList();
    }

    private int RunCheck(bool allowOneRemoval= false) => _rows.Sum(report => IsSafe(report, allowOneRemoval));

    private static int IsSafe(int[] report, bool allowOneRemoval)
    {
        if (report.Length < 2) return 1;

        if (allowOneRemoval)
        {
            return report.Where((t, i) => IsValidSequence(RemoveElement(report, i))).Any() ? 1 : 0;
        }

        var isValidSequence = IsValidSequence(report);
        return isValidSequence ? 1 : 0;
    }
    
    private static bool IsValidSequence(int[] report)
    {
        var isIncreasing = true;
        var isDecreasing = true;

        for (var i = 1; i < report.Length; i++)
        {
            var diff = Math.Abs(report[i] - report[i - 1]);

            if (diff is < 1 or > 3) return false;

            if (report[i] > report[i - 1]) isDecreasing = false;
            else if (report[i] < report[i - 1]) isIncreasing = false;

            if (!isIncreasing && !isDecreasing) return false;
        }

        return isIncreasing || isDecreasing;
    }

    private static int[] RemoveElement(int[] arr, int index)
    {
        var result = new int[arr.Length - 1];

        new Span<int>(arr, 0, index).CopyTo(result);
        new Span<int>(arr, index + 1, arr.Length - index - 1).CopyTo(result.AsSpan().Slice(index));

        return result;
    }
}