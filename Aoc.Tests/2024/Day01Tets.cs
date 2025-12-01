using FluentAssertions;

namespace Aoc.Tests._2024;

public class Day01Tets
{
    private readonly Aoc.Solutions._2024.Day01 _solution = new();
    private readonly string _filePath = $"{AppContext.BaseDirectory}/inputs/2024/Day01.txt";

    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines(_filePath);
        var solvePart1 = _solution.SolvePart1(input);
        solvePart1.Should().Be("11");
    }
    
    [Fact]
    public void SolvePart2()
    {
        var input = File.ReadAllLines(_filePath);
        var solvePart1 = _solution.SolvePart2(input);
        solvePart1.Should().Be("31");
    }
}