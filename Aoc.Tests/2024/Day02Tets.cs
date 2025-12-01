using FluentAssertions;

namespace Aoc.Tests._2024;

public class Day02Tets
{
    private readonly Aoc.Solutions._2024.Day02 _solution = new();
    private readonly string _filePath = $"{AppContext.BaseDirectory}/inputs/2024/Day02.txt";

    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines(_filePath);
        var solvePart1 = _solution.SolvePart1(input);
        solvePart1.Should().Be("2");
    }
    
    [Fact]
    public void SolvePart2()
    {
        var input = File.ReadAllLines(_filePath);
        var solvePart1 = _solution.SolvePart2(input);
        solvePart1.Should().Be("4");
    }
}