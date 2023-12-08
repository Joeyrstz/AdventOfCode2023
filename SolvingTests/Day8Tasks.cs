using SolvingLogic.Day_8;

namespace SolvingTests;

public class Day8Tasks
{
    [Fact]
    public void Task1()
    {
        var lines = Day8Solver.GetInputAsLines();
        var result = Day8Solver.SolveTask1(lines);
        Assert.Equal(15989, result);
    }
}