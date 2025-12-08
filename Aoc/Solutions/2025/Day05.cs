namespace Aoc.Solutions._2025;

public sealed class Day05 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        var ranges = ParseRanges(input);
        var freshCount = 0;

        foreach (var line in input.AsSpan())
        {
            if (line.Length == 0) continue;
            if (line.Contains('-')) continue;

            var id = long.Parse(line);
            foreach (var (start, end) in ranges)
            {
                if (id >= start && id <= end)
                {
                    freshCount++;
                    break;
                }
            }
        }

        return freshCount.ToString();
    }

    public string SolvePart2(string[] input)
    {
        var ranges = ParseRanges(input);
        
        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

        var mergedEnd = ranges[0].End;
        var mergedStart = ranges[0].Start;
        var total = 0L;

        for (var i = 1; i < ranges.Count; i++)
        {
            var (start, end) = ranges[i];
            
            if (start <= mergedEnd + 1)
            {
                // Overlapping or adjacent - extend
                if (end > mergedEnd) mergedEnd = end;
            }
            else
            {
                // Gap - finalize previous range and start new one
                total += mergedEnd - mergedStart + 1;
                mergedStart = start;
                mergedEnd = end;
            }
        }

        total += mergedEnd - mergedStart + 1;

        return total.ToString();
    }

    private static List<(long Start, long End)> ParseRanges(string[] input)
    {
        var ranges = new List<(long Start, long End)>();

        foreach (var line in input)
        {
            if (line.Length == 0) break;

            var span = line.AsSpan();
            var dashIndex = span.IndexOf('-');
            var start = long.Parse(span[..dashIndex]);
            var end = long.Parse(span[(dashIndex + 1)..]);
            ranges.Add((start, end));
        }

        return ranges;
    }
}
