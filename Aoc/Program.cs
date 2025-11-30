using Aoc.Commands;
using CliFx;

var app = new CliApplicationBuilder()
    .AddCommand<RunCommand>()
    .SetTitle("Advent of Code Runner")
    .SetExecutableName("aoc")
    .Build();

return await app.RunAsync(args);