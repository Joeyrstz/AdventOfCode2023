using System.Reflection;

namespace SolvingLogic.Day_4;

public static class Day4Solver
{
    public static string[] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 4", "Input.txt");
        return File.ReadAllLines(path);
    }
    public static int SolveTask1(string[] lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var boxes = line.Split(':')[1].Split('|');
            var left = boxes[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var right = boxes[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //Get same numbers from left and right via Linq
            var equivalentNumbers = left.Where(leftNumber => right.Contains(leftNumber)).ToList();
            if (equivalentNumbers.Count == 0) continue;

            //for one number, get one point, for every next number, double it
            var tempSum = 1;
            for (var i = 1; i < equivalentNumbers.Count; i++)
            {
                tempSum *= 2;
            }
            sum += tempSum;
        }


        return sum;
    }

    public static int SolveTask2(string[] lines)
    {
        var scratchcards = lines.Select(l => new Scratchcard(l)).ToList();
        var stack = new Stack<Scratchcard>();
        foreach (var currentScratchcard in scratchcards)
        {
            var equivalentNumbers = currentScratchcard.LeftNumbers.Count(leftNumber => currentScratchcard.RightNumbers.Contains(leftNumber));
            Console.WriteLine($"Card {currentScratchcard.CardNumber} has {equivalentNumbers} equivalent numbers");
            if (equivalentNumbers == 0)
            {
                continue;
                Console.WriteLine("Max of stack: " + stack.Max(c => c.CardNumber));
                if (stack.Max(c => c.CardNumber) > currentScratchcard.CardNumber)
                {
                    continue;
                }
                else
                {
                    //return stack.Count + scratchcards.Count;
                }
                
            }

            for (var i = 1; i <= equivalentNumbers; i++)
            {
                if (!scratchcards.Any(s => s.CardNumber == currentScratchcard.CardNumber + i))
                {
                    break;
                }

                var scratchcardToAdd = scratchcards.First(s => s.CardNumber == currentScratchcard.CardNumber + i);
                var countOfCurrentScratchcard = stack.Count(s => s.CardNumber == currentScratchcard.CardNumber) + 1;
                Console.WriteLine("Adding card " + scratchcardToAdd.CardNumber + $" to stack {countOfCurrentScratchcard} times.");
                for (var j = 0; j < countOfCurrentScratchcard; j++)
                {
                    stack.Push(scratchcardToAdd);
                }
            }
            
        }
        
        return stack.Count + scratchcards.Count;
    }
}