using SolvingLogic.Day_3;

namespace SolvingTests;

public class Day3Tasks
{
    [Fact]
    public void Task1Test()
    {
        var lines = Day3Solver.GetInput();
        var result = Day3Solver.SolveTask1(lines);
        Assert.Equal(539637, result);
    }

    [Fact]
    public void Task2Test()
    {
        var lines = Day3Solver.GetInputAsLines();
        var result = Day3Solver.SolveTask2(lines);
        Assert.Equal(999999, result);
    }
}