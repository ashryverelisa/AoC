namespace Aoc.Solutions._2025;

public sealed class Day07 : IAocSolution
{
    public string SolvePart1(string[] input) => Simulate(input, countSplits: true).ToString();

    public string SolvePart2(string[] input) => Simulate(input, countSplits: false).ToString();

    private static long Simulate(string[] grid, bool countSplits)
    {
        if (grid.Length == 0) return 0;

        var rows = grid.Length;
        var cols = grid.Max(line => line.Length);
        var (startRow, startCol) = FindStart(grid);
        
        if (startRow < 0) return 0;

        var beams = new Dictionary<int, long> { [startCol] = 1 };
        long result = 0;

        for (var row = startRow; row < rows - 1 && beams.Count > 0; row++)
        {
            var nextRow = row + 1;
            var nextBeams = new Dictionary<int, long>();

            foreach (var (col, count) in beams)
            {
                var cell = col < grid[nextRow].Length ? grid[nextRow][col] : '.';

                if (cell == '^')
                {
                    if (countSplits) result++;
                    
                    if (col > 0) AddBeam(nextBeams, col - 1, count);
                    if (col < cols - 1) AddBeam(nextBeams, col + 1, count);
                }
                else
                {
                    AddBeam(nextBeams, col, count);
                }
            }

            beams = nextBeams;
        }

        return countSplits ? result : beams.Values.Sum();
    }

    private static void AddBeam(Dictionary<int, long> beams, int col, long count)
    {
        ref var value = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(beams, col, out _);
        value += count;
    }

    private static (int row, int col) FindStart(string[] grid)
    {
        for (var r = 0; r < grid.Length; r++)
        {
            var idx = grid[r].IndexOf('S');
            if (idx >= 0) return (r, idx);
        }
        return (-1, -1);
    }
}
