using System.Text.RegularExpressions;

namespace Aoc.Solutions._2024;

public partial class Day03 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        var corruptedMemory = string.Join("\n", input);
        return GetMatches(corruptedMemory).ToString();
    }

    public string SolvePart2(string[] input)
    {
        var corruptedMemory = string.Join("\n", input);
        return GetMatches(corruptedMemory).ToString();
    }

    private int GetMatches(string corruptedMemory)
    {
        var mulRegex = MulRegex();
        var toggleRegex = ToggleRegex();
        
        var totalSum = 0;
        var isEnabled = true;
        
        var toggleMatches = toggleRegex.Matches(corruptedMemory);
        var mulMatches = mulRegex.Matches(corruptedMemory);
        
        var allMatches = new List<(int Index, string Type, Match Match)>();

        foreach (Match match in toggleMatches)
        {
            allMatches.Add((match.Index, "toggle", match));
        }

        foreach (Match match in mulMatches)
        {
            allMatches.Add((match.Index, "mul", match));
        }

        allMatches.Sort((a, b) => a.Index.CompareTo(b.Index));
        
        foreach (var entry in allMatches)
        {
            switch (entry.Type)
            {
                case "toggle":
                {
                        var toggleValue = entry.Match.Groups[1].Value;
                        isEnabled = toggleValue switch
                        {
                            "do" => true,
                            "don't" => false,
                            _ => isEnabled
                        };   
                    
                    break;
                }
                case "mul" when isEnabled:
                {
                    var x = int.Parse(entry.Match.Groups[1].Value);
                    var y = int.Parse(entry.Match.Groups[2].Value);

                    totalSum += x * y;
                    break;
                }
            }
        }

        return totalSum;
    }
    
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(do|don't)\(\)")]
    private static partial Regex ToggleRegex();
}