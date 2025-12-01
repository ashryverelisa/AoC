using Aoc.Solutions;
using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;

namespace Aoc.Commands;

[Command("run", Description = "Runs the Advent of Code solution for the provided year and day.")]
public sealed class RunCommand : ICommand
{
    [CommandOption("year", 'y', Description = "The Advent of Code year (e.g. 2024).", IsRequired = true)]
    public int Year { get; init; }

    [CommandOption("day", 'd', Description = "The Advent of Code day (1-25).", IsRequired = true)]
    public int Day { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        ValidateInputs();

        if (!SolutionRegistry.TryCreate(Year, Day, out var solution))
        {
            throw new CommandException($"Solution for year {Year} day {Day} not found. Expect file 'Solutions/{Year}/Day{Day}.cs'.");
        }

        var input = LoadInput(Year, Day);

        var part1 = solution?.SolvePart1(input);
        var part2 = solution?.SolvePart2(input);

        await console.Output.WriteLineAsync($"Year {Year} Day {Day:00}");
        await console.Output.WriteLineAsync($"Part 1: {part1}");
        await console.Output.WriteLineAsync($"Part 2: {part2}");
    }

    private void ValidateInputs()
    {
        if (Year < 2015)
        {
            throw new CommandException("Year must be 2015 or later.");
        }

        if (Day is < 1 or > 25)
        {
            throw new CommandException("Day must be between 1 and 25.");
        }
    }

    private static string[] LoadInput(int year, int day)
    {
        var inputPath = Path.Combine("inputs", year.ToString(), $"{day}.txt");
        return !File.Exists(inputPath) ? throw new CommandException($"Input file not found at '{inputPath}'.") : File.ReadAllLines(inputPath);
    }
}
