using FluentAssertions;

namespace Aoc.Tests._2025;

public class Day04Tets
{
    private readonly Aoc.Solutions._2025.Day04 _solution = new();
    private readonly string _filePath = $"{AppContext.BaseDirectory}/inputs/2025/Day04.txt";
    
    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines(_filePath);
        var result = _solution.SolvePart1(input);
        result.Should().Be("13");
    }

    [Fact]
    public void SolvePart2()
    {
        var input = File.ReadAllLines(_filePath);
        var result = _solution.SolvePart2(input);
        result.Should().Be("43");
    }
}