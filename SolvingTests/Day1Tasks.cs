using SolvingLogic.Day_1;

namespace SolvingTests;

public class Day1Tasks
{
    [Fact]
    public void Task1Test()
    {
        var lines = Day1Solver.GetInput();
        var result = Day1Solver.SolveTask1(lines);
        Assert.Equal(56506, result);
    }
    
    [Fact]
    public void Task2Test()
    {
        var lines = Day1Solver.GetInput();
        var result = Day1Solver.SolveTast2(lines);
        Assert.Equal(56017, result);
    }
}