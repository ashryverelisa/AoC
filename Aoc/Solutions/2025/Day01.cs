using System.Runtime.CompilerServices;

namespace Aoc.Solutions._2025;

public sealed class Day01 : IAocSolution
{
    private const int DialSize = 100;
    private const int StartPoint = 50;

    public string SolvePart1(string[] input)
    {
        var position = StartPoint;
        var zeroHits = 0;

        foreach (var line in input)
        {
            ProcessLine(line.AsSpan(), ref position, ref zeroHits, countIntermediate: false);
        }

        return zeroHits.ToString();
    }

    public string SolvePart2(string[] input)
    {
        var position = StartPoint;
        var zeroHits = 0;

        foreach (var line in input)
        {
            ProcessLine(line.AsSpan(), ref position, ref zeroHits, countIntermediate: true);
        }

        return zeroHits.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProcessLine(ReadOnlySpan<char> line, ref int position, ref int zeroHits, bool countIntermediate)
    {
        while (!line.IsEmpty)
        {
            var start = 0;
            while (start < line.Length && char.IsWhiteSpace(line[start]))
                start++;

            if (start >= line.Length)
                break;

            line = line[start..];

            var end = 0;
            while (end < line.Length && !char.IsWhiteSpace(line[end]))
                end++;

            var token = line[..end];
            line = line[end..];

            ProcessToken(token, ref position, ref zeroHits, countIntermediate);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProcessToken(ReadOnlySpan<char> token, ref int position, ref int zeroHits, bool countIntermediate)
    {
        if (token.Length < 2)
            return;

        var dirChar = token[0];
        var direction = (dirChar | 0x20) == 'l' ? -1 : 1;

        if (!int.TryParse(token[1..], out var amount) || amount < 0)
            return;

        if (countIntermediate)
        {
            zeroHits += CountIntermediateZeroHits(position, direction, amount);
        }

        position = WrapDial(position + direction * amount);

        if (!countIntermediate && position == 0)
        {
            zeroHits++;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountIntermediateZeroHits(int start, int direction, int amount)
    {
        if (amount == 0)
            return 0;

        var firstHitDistance = direction < 0
            ? start                         
            : (DialSize - start) % DialSize;

        if (firstHitDistance == 0)
            firstHitDistance = DialSize;

        if (amount < firstHitDistance)
            return 0;

        return 1 + (amount - firstHitDistance) / DialSize;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int WrapDial(int value)
    {
        var wrapped = value % DialSize;
        return wrapped < 0 ? wrapped + DialSize : wrapped;
    }
}
