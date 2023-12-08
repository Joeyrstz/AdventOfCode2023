using SolvingLogic.Day_7;

namespace SolvingTests;

public class Day7Tasks
{
    [Fact]
    public void SolveTask1Test()
    {
        var lines = Day7Solver.GetInput();
        var result = Day7Solver.SolveTask1(lines);
        Assert.Equal(248836197, result);
    }
    [Fact]
    public void SolveTask2Test()
    {
        var lines = Day7Solver.GetInput();
        var result = Day7Solver.SolveTask2(lines);
        Assert.Equal(251195607, result);
    }
}