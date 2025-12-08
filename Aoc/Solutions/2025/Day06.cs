namespace Aoc.Solutions._2025;

public sealed class Day06 : IAocSolution
{
    public string SolvePart1(string[] input) => Solve(input, part2: false);
    
    public string SolvePart2(string[] input) => Solve(input, part2: true);

    private static string Solve(string[] input, bool part2)
    {
        if (input.Length == 0) return "0";

        var maxLength = input.Max(line => line.Length);
        var paddedLines = input.Select(line => line.PadRight(maxLength)).ToArray();
        var numRows = input.Length - 1;
        
        long grandTotal = 0;
        var col = part2 ? maxLength - 1 : 0;

        while (part2 ? col >= 0 : col < maxLength)
        {
            while ((part2 ? col >= 0 : col < maxLength) && IsColumnAllSpaces(paddedLines, col))
                col += part2 ? -1 : 1;

            if (part2 ? col < 0 : col >= maxLength) break;

            var endCol = col;
            while ((part2 ? col >= 0 : col < maxLength) && !IsColumnAllSpaces(paddedLines, col))
                col += part2 ? -1 : 1;
            
            var startCol = part2 ? col + 1 : endCol;
            endCol = part2 ? endCol : col - 1;

            var operation = FindOperation(paddedLines[^1], startCol, endCol);
            if (operation != '+' && operation != '*') continue;

            long result = operation == '*' ? 1 : 0;
            
            if (part2)
            {
                for (var c = endCol; c >= startCol; c--)
                {
                    var num = ReadVerticalNumber(paddedLines, c, numRows);
                    if (num >= 0)
                        result = operation == '*' ? result * num : result + num;
                }
            }
            else
            {
                for (var row = 0; row < numRows; row++)
                {
                    var span = paddedLines[row].AsSpan(startCol, endCol - startCol + 1).Trim();
                    if (span.Length > 0 && long.TryParse(span, out var num))
                        result = operation == '*' ? result * num : result + num;
                }
            }

            grandTotal += result;
        }

        return grandTotal.ToString();
    }

    private static char FindOperation(string opLine, int start, int end)
    {
        for (var c = start; c <= end; c++)
        {
            if (opLine[c] is '+' or '*')
                return opLine[c];
        }
        return ' ';
    }

    private static long ReadVerticalNumber(string[] lines, int col, int numRows)
    {
        long num = 0;
        var hasDigit = false;
        
        for (var row = 0; row < numRows; row++)
        {
            var ch = lines[row][col];
            
            if (!char.IsAsciiDigit(ch)) continue;
            num = num * 10 + (ch - '0');
            hasDigit = true;
        }
        
        return hasDigit ? num : -1;
    }

    private static bool IsColumnAllSpaces(string[] lines, int col)
    {
        return lines.All(line => col >= line.Length || line[col] == ' ');
    }
}
