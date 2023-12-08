using System.Reflection;

namespace SolvingLogic.Day_3;

public static class Day3Solver
{
    public static char[][] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 3", "Input.txt");
        var lines = File.ReadAllLines(path);
        var list = new List<char[]>();
        foreach (var line in lines)
        {
            list.Add(line.ToCharArray());
        }

        return list.ToArray();
    }
    public static string[] GetInputAsLines()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 3", "Input.txt");
        var lines = File.ReadAllLines(path);
        return lines;
    }
    
    public static int SolveTask1(char[][] lines)
    {
        var sum = 0;
        var width = lines[0].Length;
        var height = lines.Length;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var value = lines[i][j];
                if(value is '.') continue;
                
                if(!char.IsDigit(value)) continue;

                var tempString = "";
                tempString += value;
                var tempJ = j + 1;
                while (tempJ < lines[0].Length && char.IsDigit(lines[i][tempJ++]))
                {
                    tempString += lines[i][tempJ - 1];
                }
                Console.WriteLine("Found number: " + tempString);
                
                var symbolFound = SymbolFound(lines, i, j, tempJ - 1);
                if (symbolFound)
                {
                    PrintTouchingArray(lines, i, j);
                    sum += int.Parse(tempString);
                }

                j = tempJ - 1;
            }
            
            
        }

        return sum;
    }
    
    public static bool SymbolFound(char[][] lines, int i, int j, int endJ)
    {
        if (j > 0 && lines[i][j-1] is not '.' && char.IsDigit(lines[i][j - 1]) is false)
        {
            return true;
        }

        if (endJ < lines[0].Length && lines[i][endJ] is not '.' && char.IsDigit(lines[i][endJ]) is false)
        {
            return true;
        }
        
        if (i is not 0)
        {
            
            var tempJ = 0;
            if (j > 0)
            {
                tempJ = j - 1;
            }
            var endbound = endJ;
            if (endJ >= lines[0].Length)
            {
                endbound = lines[0].Length - 1;
            }
            for (var iterator = tempJ; iterator <= endbound; iterator++)
            {
                if(lines[i - 1][iterator] is not '.' && char.IsDigit(lines[i - 1][iterator]) is false)
                    return true;
            }
        }

        if (i < lines.Length - 1)
        {
            var tempJ = 0;
            if (j > 0)
            {
                tempJ = j - 1;
            }
            var endbound = endJ;
            if (endJ >= lines[0].Length)
            {
                endbound = lines[0].Length - 1;
            }
            for (var iterator = tempJ; iterator <= endbound; iterator++)
            {
                if(lines[i + 1][iterator] is not '.' && char.IsDigit(lines[i + 1][iterator]) is false)
                    return true;
            }
        }


        return false;
    }


    public static int SolveTask2(string[] lines)
    {
        int totalSum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '*')
                {
                    var adjacentNumbers = GetAdjacentNumbers(lines, i, j);
                    if (adjacentNumbers.Count == 2)
                    {
                        totalSum += adjacentNumbers[0] * adjacentNumbers[1];
                    }
                }
            }
        }

        return totalSum;
    }

    



    private static void PrintTouchingArray(char[][] lines, int i, int j)
    {
        var width = lines[0].Length;
        var height = lines.Length;
        var tempJ = j - 1;
        var tempI = i - 1;
        for (var iterator = tempI; iterator <= tempI + 2; iterator++)
        {
            for (var iterator2 = tempJ; iterator2 <= tempJ + 5; iterator2++)
            {
                if (iterator < 0 || iterator >= height || iterator2 < 0 || iterator2 >= width)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(lines[iterator][iterator2]);
                }
            }
            Console.WriteLine();
        }
    }
    
    private static List<int> GetAdjacentNumbers(string[] schematic, int row, int column)
    {
        var numbers = new List<int>();

        // Extract horizontal number to the left
        int leftNumber = GetNumberFromPosition(schematic, row, column, false);
        if (leftNumber != 0)
        {
            numbers.Add(leftNumber);
        }

        // Extract horizontal number to the right
        int rightNumber = GetNumberFromPosition(schematic, row, column, true);
        if (rightNumber != 0)
        {
            numbers.Add(rightNumber);
        }

        return numbers;
    }

    private static int GetNumberFromPosition(string[] schematic, int row, int column, bool goRight)
    {
        string numberStr = "";
        int currentColumn = goRight ? column + 1 : column - 1;

        while (currentColumn >= 0 && currentColumn < schematic[row].Length && char.IsDigit(schematic[row][currentColumn]))
        {
            if (goRight)
            {
                numberStr += schematic[row][currentColumn];
            }
            else
            {
                numberStr = schematic[row][currentColumn] + numberStr;
            }
            currentColumn += goRight ? 1 : -1;
        }

        return numberStr == "" ? 0 : int.Parse(numberStr);
    }
    
    
}