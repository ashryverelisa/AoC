namespace Aoc.Solutions._2025;

public sealed class Day02 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        long sum = 0;
        foreach (var (start, end) in ParseRanges(input))
        {
            for (var value = start; value <= end; value++)
            {
                if (IsRepeatedPattern(value, exactTwo: true))
                {
                    sum += value;
                }
            }
        }

        return sum.ToString();
    }

    public string SolvePart2(string[] input)
    {
        long sum = 0;
        foreach (var (start, end) in ParseRanges(input))
        {
            for (var value = start; value <= end; value++)
            {
                if (IsRepeatedPattern(value, exactTwo: false))
                {
                    sum += value;
                }
            }
        }

        return sum.ToString();
    }

    private static IEnumerable<(long Start, long End)> ParseRanges(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var segments = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var segment in segments)
            {
                var trimmed = segment.Trim();
                if (trimmed.Length == 0)
                {
                    continue;
                }

                var bounds = trimmed.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (bounds.Length != 2)
                {
                    throw new InvalidDataException($"Invalid range: {trimmed}");
                }

                var start = long.Parse(bounds[0]);
                var end = long.Parse(bounds[1]);
                if (end < start)
                {
                    throw new InvalidDataException($"Range end before start: {trimmed}");
                }

                yield return (start, end);
            }
        }
    }

    private static bool IsRepeatedPattern(long value, bool exactTwo)
    {
        var digits = value.ToString();
        var totalLength = digits.Length;

        for (var chunkLength = 1; chunkLength <= totalLength / 2; chunkLength++)
        {
            if (totalLength % chunkLength != 0)
            {
                continue;
            }

            var repeatCount = totalLength / chunkLength;
            if (repeatCount < 2)
            {
                continue;
            }

            if (exactTwo && repeatCount != 2)
            {
                continue;
            }

            var chunk = digits.AsSpan(0, chunkLength);
            var matches = true;
            for (var index = chunkLength; index < totalLength; index += chunkLength)
            {
                if (!digits.AsSpan(index, chunkLength).SequenceEqual(chunk))
                {
                    matches = false;
                    break;
                }
            }

            if (!matches)
            {
                continue;
            }

            if (!exactTwo || repeatCount == 2)
            {
                return true;
            }
        }

        return false;
    }
}
