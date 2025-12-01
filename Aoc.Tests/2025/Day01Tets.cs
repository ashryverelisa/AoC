using FluentAssertions;

namespace Aoc.Tests._2025;

public class Day01Tets
{
    private readonly Aoc.Solutions._2025.Day01 _solution = new();
    private readonly string _filePath = $"{AppContext.BaseDirectory}/inputs/2025/Day01.txt";
    
    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines(_filePath);
        var result = _solution.SolvePart1(input);
        result.Should().Be("3");
    }

    [Fact]
    public void SolvePart2()
    {
        var input = File.ReadAllLines(_filePath);
        var result = _solution.SolvePart2(input);
        result.Should().Be("6");
    }
}