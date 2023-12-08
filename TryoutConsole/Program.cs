// See https://aka.ms/new-console-template for more information

using SolvingLogic.Day_5;

Console.WriteLine("Lets Calculate...");
var lines = Day5Solver.GetInput();
var result = Day5Solver.SolveTask2(lines);
Console.WriteLine("Final Result: " + result);
Console.ReadKey();