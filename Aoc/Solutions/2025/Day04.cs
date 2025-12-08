namespace Aoc.Solutions._2025;

public sealed class Day04 : IAocSolution
{
    private static readonly (int DRow, int DCol)[] Offsets =
    [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1)
    ];

    public string SolvePart1(string[] input)
    {
        var rolls = ParseRolls(input);
        return rolls.Count(r => CountNeighbors(rolls, r) < 4).ToString();
    }

    public string SolvePart2(string[] input)
    {
        var rolls = ParseRolls(input);
        var removed = 0;

        while (true)
        {
            var toRemove = rolls.Where(r => CountNeighbors(rolls, r) < 4).ToList();
            if (toRemove.Count == 0) break;

            rolls.ExceptWith(toRemove);
            removed += toRemove.Count;
        }

        return removed.ToString();
    }

    private static HashSet<(int Row, int Col)> ParseRolls(string[] input)
    {
        var rolls = new HashSet<(int Row, int Col)>();
        
        for (var row = 0; row < input.Length; row++)
        {
            var line = input[row].AsSpan();
            for (var col = 0; col < line.Length; col++)
            {
                if (line[col] == '@')
                    rolls.Add((row, col));
            }
        }

        return rolls;
    }

    private static int CountNeighbors(HashSet<(int Row, int Col)> rolls, (int Row, int Col) pos)
    {
        var count = 0;
        foreach (var (dRow, dCol) in Offsets)
        {
            if (rolls.Contains((pos.Row + dRow, pos.Col + dCol)))
                count++;
        }

        return count;
    }
}
