using SolvingLogic.Day_2;

namespace SolvingTests;

public class Day2Tasks
{
    [Fact]
    public void Task1Test()
    {
        var lines = Day2Solver.GetInput();
        var result = Day2Solver.SolveTask1(lines);
        Assert.Equal(2406, result);
    }
    
    [Fact]
    public void Task2Test()
    {
        var lines = Day2Solver.GetInput();
        var result = Day2Solver.SolveTask2(lines);
        Assert.Equal(78375, result);
    }
}