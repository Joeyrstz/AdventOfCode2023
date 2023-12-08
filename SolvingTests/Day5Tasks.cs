using SolvingLogic.Day_5;

namespace SolvingTests;

public class Day5Tasks
{
    [Fact]
    public void SolveTask1Test()
    {
        var lines = Day5Solver.GetInput();
        var result = Day5Solver.SolveTask1(lines);
        Assert.Equal(111627841, result);
    }
    
    [Fact]
    public void SolveTask2Test()
    {
        var lines = Day5Solver.GetInput();
        var result = Day5Solver.SolveTask2(lines);
        Assert.Equal(999999, result);
    }
}