namespace Aoc.Solutions._2025;

public sealed class Day01 : IAocSolution
{
    private const int DialSize = 100;
    private const int StartPoint = 50;
    
    public string SolvePart1(string[] input)
    {
        var zeroHits = CountZeroHits(input, StartPoint);
        return zeroHits.ToString();
    }

    public string SolvePart2(string[] input)
    {
        var zeroHits = CountZeroHits(input, StartPoint, true);
        return zeroHits.ToString();
    }

    private int CountZeroHits(string[] input, int startPoint, bool countIntermediate = false)
    {
        var position = WrapDial(startPoint);
        var zeroHits = 0;

        foreach (var rotation in ParseRotations(input))
        {
            if (countIntermediate)
            {
                zeroHits += CountIntermediateZeroHits(position, rotation);
            }

            position = ApplyRotation(position, rotation);

            if (!countIntermediate && position == 0)
            {
                zeroHits++;
            }
        }

        return zeroHits;
    }

    private int ApplyRotation(int current, Rotation rotation)
    {
        var steps = rotation.Direction switch
        {
            'L' => -rotation.Amount,
            'R' => rotation.Amount,
            _ => throw new InvalidDataException($"Unsupported direction '{rotation.Direction}'.")
        };

        return WrapDial(current + steps);
    }

    private static int CountIntermediateZeroHits(int start, Rotation rotation)
    {
        if (rotation.Amount == 0)
        {
            return 0;
        }

        var firstHitDistance = rotation.Direction switch
        {
            'L' => start % DialSize,
            'R' => (DialSize - start) % DialSize,
            _ => throw new InvalidDataException($"Unsupported direction '{rotation.Direction}'.")
        };

        if (firstHitDistance == 0)
        {
            firstHitDistance = DialSize;
        }

        if (rotation.Amount < firstHitDistance)
        {
            return 0;
        }

        return 1 + (rotation.Amount - firstHitDistance) / DialSize;
    }

    private static int WrapDial(int value)
    {
        var wrapped = value % DialSize;
        return wrapped < 0 ? wrapped + DialSize : wrapped;
    }

    private static IEnumerable<Rotation> ParseRotations(string[] input)
    {
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                yield return ParseRotation(token);
            }
        }
    }

    private static Rotation ParseRotation(string token)
    {
        var trimmed = token.Trim();
        if (trimmed.Length < 2)
        {
            throw new InvalidDataException($"Rotation token '{token}' is too short.");
        }

        var direction = char.ToUpperInvariant(trimmed[0]);
        if (direction is not ('L' or 'R'))
        {
            throw new InvalidDataException($"Unknown rotation direction '{direction}'.");
        }

        if (!int.TryParse(trimmed.AsSpan(1), out var amount) || amount < 0)
        {
            throw new InvalidDataException($"Rotation token '{token}' has an invalid amount.");
        }

        return new Rotation(direction, amount);
    }

    private readonly record struct Rotation(char Direction, int Amount);
}
