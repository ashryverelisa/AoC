using FluentAssertions;

namespace Aoc.Tests._2024;

public class Day01Tets
{
    private readonly Aoc.Solutions._2024.Day01 _solution = new();

    [Fact]
    public void SolvePart1()
    {
        var input = File.ReadAllLines($"{AppContext.BaseDirectory}/inputs/2024/Day01.txt");
        var solvePart1 = _solution.SolvePart1(input);
        solvePart1.Should().Be("11");
    }
}