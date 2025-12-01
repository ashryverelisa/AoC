using FluentAssertions;

namespace Aoc.Tests._2024;

public class Day03Tets
{
    private readonly Aoc.Solutions._2024.Day03 _solution = new();

    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines($"{AppContext.BaseDirectory}/inputs/2024/Day03_Part1.txt");
        var solvePart1 = _solution.SolvePart1(input);
        solvePart1.Should().Be("161");
    }
    
    [Fact]
    public void SolvePart2()
    {
        var input = File.ReadAllLines($"{AppContext.BaseDirectory}/inputs/2024/Day03_Part2.txt");
        var solvePart1 = _solution.SolvePart2(input);
        solvePart1.Should().Be("48");
    }
}