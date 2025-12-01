namespace Aoc.Solutions._2024;

public sealed class Day01 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        var leftPart = new List<int>();
        var rightPart = new List<int>();

        foreach (var line in input)
        {
            var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            leftPart.Add(int.Parse(parts[0]));
            rightPart.Add(int.Parse(parts[1]));
        }

        leftPart.Sort();
        rightPart.Sort();

        return leftPart.Select((t, i) => Math.Abs(t - rightPart[i])).Sum().ToString();
    }

    public string SolvePart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
