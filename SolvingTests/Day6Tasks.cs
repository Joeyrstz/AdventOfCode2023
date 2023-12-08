using SolvingLogic.Day_6;

namespace SolvingTests;

public class Day6Tasks
{
    [Fact]
    public void SolveTask1Test()
    {
        var result = Day6Solver.SolveTask1();
        Assert.Equal(1710720, result);
    }
    
    [Fact]
    public void SolveTask2Test()
    {
        var result = Day6Solver.SolveTask2();
        Assert.Equal(99999, result);
    }
}