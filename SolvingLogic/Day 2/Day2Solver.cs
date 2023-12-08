using System.Reflection;

namespace SolvingLogic.Day_2;

public static class Day2Solver
{
    private static Dictionary<string, int> maxColors = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };
    public static string[] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 2", "Input.txt");
        return File.ReadAllLines(path);
    }
    
    public static int SolveTask1(string[] lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var temp = line.Split(':');
            var gameHeader = temp[0];
            var combinations = temp[1];
            var splittedCombinations = combinations.Split(';');
            var failure = false;
            foreach (var gameRound in splittedCombinations)
            {
                var boxes = gameRound.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < boxes.Length; i+= 2)
                {
                    if(int.Parse(boxes[i]) > maxColors[boxes[i + 1].Replace(",", string.Empty)])
                    {
                        failure = true;
                        Console.WriteLine("Game " + gameHeader + " failed at round " + gameRound);
                        break;
                    }
                }
            }
            if (failure is false)
            {
                var numValue = int.Parse(gameHeader.Replace("Game ", string.Empty));
                sum+= numValue;
            }
        }


        return sum;
    }

    public static int SolveTask2(string[] lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var temp = line.Split(':');
            var gameHeader = temp[0];
            var combinations = temp[1];
            var splittedCombinations = combinations.Split(';');
            var failure = false;
            
            var maxDict = new Dictionary<string, int>()
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 },
            };
            foreach (var gameRound in splittedCombinations)
            {
                var boxes = gameRound.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < boxes.Length; i+= 2)
                {
                    var color = boxes[i + 1].Replace(",", string.Empty);
                    var value = int.Parse(boxes[i]);
                    if (maxDict[color] < value)
                    {
                        maxDict[color] = value;
                    }
                }
            }

            var multiplication = maxDict.Aggregate(1, (current, keyValue) => current * keyValue.Value);
            sum += multiplication;
        }


        return sum;
    }
}