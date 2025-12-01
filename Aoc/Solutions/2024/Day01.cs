namespace Aoc.Solutions._2024;

public sealed class Day01 : IAocSolution
{
    private readonly List<int> _leftPart = [];
    private readonly List<int> _rightPart = [];
    
    public string SolvePart1(string[] input)
    {
        MappingLeftAndRightParts(input);
        return _leftPart.Select((t, i) => Math.Abs(t - _rightPart[i])).Sum().ToString();
    }

    public string SolvePart2(string[] input)
    {
        MappingLeftAndRightParts(input);
        return CalculateSimilarity().ToString();
    }
    
    private void Reset()
    {
        _leftPart.Clear();
        _rightPart.Clear();
    }
    
    private void MappingLeftAndRightParts(string[] input)
    {
        Reset();
        
        foreach (var line in input)
        {
            var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            _leftPart.Add(int.Parse(parts[0]));
            _rightPart.Add(int.Parse(parts[1]));
        }

        _leftPart.Sort();
        _rightPart.Sort();
    }
    
    private int CalculateSimilarity()
    {
        var rightPartFrequency = _rightPart.GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

        var similarityScore = 0;

        foreach (var t in (_leftPart))
        {
            if (rightPartFrequency.TryGetValue(t, out var count))
            {
                similarityScore += count * t;
            }
        }
        
        return similarityScore;
    }
}