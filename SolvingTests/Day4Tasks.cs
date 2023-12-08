using SolvingLogic.Day_4;

namespace SolvingTests;

public class Day4Tasks
{
    [Fact]
    public void Task1Test()
    {
        var lines = Day4Solver.GetInput();
        var result = Day4Solver.SolveTask1(lines);
        Assert.Equal(22193, result);
    }

    [Fact]
    public void Task2Test()
    {
        var lines = Day4Solver.GetInput();
        var result = Day4Solver.SolveTask2(lines);
        Assert.Equal(0, result);
    }
}