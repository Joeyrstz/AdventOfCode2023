using System.Reflection;

namespace SolvingLogic.Day_1;

public static class Day1Solver
{
    public static Dictionary<int, string> dic = new Dictionary<int, string>()
    {
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
    };
    public static string[] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 1", "Input.txt");
        return File.ReadAllLines(path);
    }

    public static int SolveTask1(string[] lines)
    {
        var sum = 0;
        
        foreach (var line in lines)
        {
            char? first = null;
            char? last = null;

            foreach (var character in line.ToCharArray())
            {
                if (int.TryParse(character.ToString(), out _))
                {
                    if (first is null)
                    {
                        first = character;
                        last = character;
                        continue;
                    }
                    last = character;
                }
            }
            
            sum += int.Parse(first + last.ToString());
        }

        return sum;
    }

    public static int SolveTast2(string[] lines)
    {
        var sum = 0;
        
        foreach (var line in lines)
        {
            int? first = null;
            int? last = null;

            var charArray = line.ToCharArray();
            for (var i = 0; i < charArray.Length; i++)
            {
                var character = charArray[i];
                if (int.TryParse(character.ToString(), out _))
                {
                    if (first is null)
                    {
                        first = int.Parse(character.ToString());
                        last = int.Parse(character.ToString());
                        continue;
                    }

                    last = int.Parse(character.ToString());
                }
                else
                {
                    var subString = line.Substring(i);
                    if (!dic.Any(x => subString.StartsWith(x.Value))) continue;
                    
                    var result = dic.First(x => subString.StartsWith(x.Value));
                    if (first is null)
                    {
                        first = result.Key;
                        last = result.Key;
                        continue;
                    }

                    last = result.Key;
                }
            }

            sum += int.Parse($"{first}{last}");
        }

        return sum;
    }
    
}