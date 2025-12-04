namespace Aoc.Solutions._2025;

using System.Collections.Generic;
using System.Globalization;

public sealed class Day02 : IAocSolution
{
    private static readonly object RangeCacheLock = new();
    private static readonly Dictionary<string, List<(long Start, long End)>> RangeCache = new();

    public string SolvePart1(string[] input)
    {
        return SumMatchingValues(input, exactTwo: true).ToString();
    }

    public string SolvePart2(string[] input)
    {
        return SumMatchingValues(input, exactTwo: false).ToString();
    }

    private static long SumMatchingValues(string[] input, bool exactTwo)
    {
        long sum = 0;
        foreach (var (start, end) in ParseRanges(input))
        {
            for (var value = start; value <= end; value++)
            {
                if (IsRepeatedPattern(value, exactTwo))
                {
                    sum += value;
                }
            }
        }

        return sum;
    }

    private static IReadOnlyList<(long Start, long End)> ParseRanges(string[] input)
    {
        var cacheKey = string.Join('\n', input);
        lock (RangeCacheLock)
        {
            if (RangeCache.TryGetValue(cacheKey, out var cached))
            {
                return cached;
            }
        }

        var ranges = new List<(long, long)>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var remaining = line.AsSpan();
            while (!remaining.IsEmpty)
            {
                var commaIndex = remaining.IndexOf(',');
                var segment = commaIndex < 0 ? remaining : remaining[..commaIndex];
                remaining = commaIndex < 0 ? ReadOnlySpan<char>.Empty : remaining[(commaIndex + 1)..];

                segment = segment.Trim();
                if (segment.IsEmpty)
                {
                    continue;
                }

                var dashIndex = segment.IndexOf('-');
                if (dashIndex <= 0 || dashIndex >= segment.Length - 1)
                {
                    throw new InvalidDataException($"Invalid range: {segment.ToString()}");
                }

                var startSpan = segment[..dashIndex];
                var endSpan = segment[(dashIndex + 1)..];
                if (!long.TryParse(startSpan, NumberStyles.Integer, CultureInfo.InvariantCulture, out var start) ||
                    !long.TryParse(endSpan, NumberStyles.Integer, CultureInfo.InvariantCulture, out var end))
                {
                    throw new InvalidDataException($"Invalid range: {segment.ToString()}");
                }

                if (end < start)
                {
                    throw new InvalidDataException($"Range end before start: {segment.ToString()}");
                }

                ranges.Add((start, end));
            }
        }

        lock (RangeCacheLock)
        {
            RangeCache[cacheKey] = ranges;
        }

        return ranges;
    }

    private static bool IsRepeatedPattern(long value, bool exactTwo)
    {
        Span<char> buffer = stackalloc char[20];
        if (!value.TryFormat(buffer, out var length))
        {
            return false;
        }

        var digits = buffer[..length];
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

            var chunk = digits[..chunkLength];
            var matches = true;
            for (var index = chunkLength; index < totalLength; index += chunkLength)
            {
                if (!digits.Slice(index, chunkLength).SequenceEqual(chunk))
                {
                    matches = false;
                    break;
                }
            }

            if (matches)
            {
                return true;
            }
        }

        return false;
    }
}
