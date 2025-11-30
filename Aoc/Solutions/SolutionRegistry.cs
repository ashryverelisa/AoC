namespace Aoc.Solutions;

internal static class SolutionRegistry
{
    public static bool TryCreate(int year, int day, out IAocSolution? solution)
    {
        var typeName = $"Aoc.Solutions._{year}.Day{day:00}";
        var type = typeof(SolutionRegistry).Assembly.GetType(typeName);

        if (type is null || !typeof(IAocSolution).IsAssignableFrom(type))
        {
            solution = null;
            return false;
        }

        solution = (IAocSolution)Activator.CreateInstance(type)!;
        return true;
    }
}
