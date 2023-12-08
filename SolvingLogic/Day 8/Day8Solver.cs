using System.Reflection;

namespace SolvingLogic.Day_8;

public static class Day8Solver
{
    public static string[] GetInputAsLines()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 8", "Input.txt");
        var lines = File.ReadAllLines(path);
        return lines;
    }

    public static int SolveTask1(string[] lines)
    {
        var steps = 0;
        var inputOrders = lines[0].ToCharArray();
        var nodeLines = lines[2..];
        
        
        var graph = new Graph();
        

        foreach (var nodeLine in nodeLines)
        {
            var splitted = nodeLine.Split("=");
            var id = splitted[0].Trim();
            var splittedNavigations = splitted[1].Split(", ");
            var left = splittedNavigations[0].Replace("(", string.Empty).Trim();
            var right = splittedNavigations[1].Replace(")", string.Empty).Trim();
            
            var currentNode = graph.GetOrCreate(id);
            var leftNode = graph.GetOrCreate(left);
            var rightNode = graph.GetOrCreate(right);
            
            // Set the properties on the currentNode
            currentNode.LeftNode = leftNode;
            currentNode.RightNode = rightNode;
        }
        var currentLocation = graph.Nodes["AAA"];
        var directions = new Queue<char>();

        while (currentLocation.Id != "ZZZ")
        {
            Console.WriteLine("Current location: " + currentLocation.Id);
            Console.WriteLine("currentLocation.LeftNode: " + currentLocation.LeftNode?.Id);
            Console.WriteLine("currentLocation.RightNode: " + currentLocation.RightNode?.Id);
            if (directions.Count == 0)
            {
                foreach (var inputOrder in inputOrders)
                {
                    directions.Enqueue(inputOrder);
                }
            }
            var direction = directions.Dequeue();
            Console.WriteLine("Direction is " + direction);
            currentLocation = currentLocation.Traverse(direction);
            Console.WriteLine("Going " + direction + " to " );
            steps++;
        }
        
        
        


        return steps;
    }
    
}